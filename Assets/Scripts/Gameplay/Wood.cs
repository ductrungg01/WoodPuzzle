using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WoodPuzzle.Core;

public class Wood : MonoBehaviour
{
    [SerializeField] private GameObject boltsParent;
    [SerializeField] private GameObject holdPrefab;
    [SerializeField] private GameObject boltPrefab;

    private Level level;
    private Transform boltParentHierachy;
    private Transform holdParentHierachy;

    // Save all used positon of hold/bolt when instantiate,
    // help detect a position is used or not, a pos is very close with other pos also not be used
    private static List<Vector3> usedPositions = new List<Vector3>();

    private void Start()
    {
        level = GetComponentInParent<Level>();
        boltParentHierachy = level.boltParentInHierachy;
        holdParentHierachy = level.holdParentInHierachy;

        level.AddWood();

        GenerateHoldAndBolts();

        GetComponent<Rigidbody2D>().gravityScale = 1;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DeathZone"))
        {
            AudioManager.Instance.PlaySFX("WoodDropped");

            level.RemoveWood();

            // enable the polygon collider for improve performance
            this.gameObject.GetComponent<PolygonCollider2D>().enabled = false;

            // Disable after 0.5 second
            StartCoroutine(DisableAfterDelay(0.5f));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bolt"))
        {
            // Get the collision force based on the relative velocity of the objects
            float collisionForce = collision.relativeVelocity.magnitude;

            // Adjust the volume based on the collision force
            float volume = Mathf.Clamp01(collisionForce / 200.0f); // Assume 200.0f is the max force expected

            // Call PlaySFX with the appropriate volume level
            AudioManager.Instance.PlaySFX("WoodHitBolt", volume);
        }
    }

    private IEnumerator DisableAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        gameObject.SetActive(false);
    }

    private void GenerateHoldAndBolts()
    {
        if (boltsParent == null || holdPrefab == null || boltPrefab == null)
        {
            Debug.LogError("{bolt parent} or {hold prefab} or {bolt prefab} gameobject is null");
            return;
        }

        // Get all transform in children of boltsParent, exclude the tranform of boltsParent
        Transform[] transforms = boltsParent.GetComponentsInChildren<Transform>()
                                   .Where(t => t != boltsParent.transform)
                                   .ToArray();

        foreach (Transform t in transforms)
        {
            Vector3 position = new Vector3(t.position.x, t.position.y, t.position.z);

            // If current pos is very close another pos
            if (CheckIsUsedPos(position)) continue;

            usedPositions.Add(position);

            GameObject aHold = Instantiate(holdPrefab, position, Quaternion.identity, holdParentHierachy);
            GameObject aBolt = Instantiate(boltPrefab, position, Quaternion.identity, boltParentHierachy);
        }
    }

    private bool CheckIsUsedPos(Vector3 position)
    {
        float minimumDistance = 2f;
        foreach (Vector3 usedPos in usedPositions)
        {
            float distance = Vector3.Distance(position, usedPos);
            if (distance <= minimumDistance)
            {
                return true;
            }
        }

        return false;
    }

    public static void ResetWoodUsedPosition()
    {
        usedPositions.Clear();
    }
}
