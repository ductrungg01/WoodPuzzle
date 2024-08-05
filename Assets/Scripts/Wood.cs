using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Wood : MonoBehaviour
{
    public bool IsFallOver {  get; private set; }

    [SerializeField] private GameObject boltsParent;
    [SerializeField] private GameObject holdPrefab;
    [SerializeField] private GameObject boltPrefab;

    [HideInInspector] public List<GameObject> holds = new List<GameObject>();
    [HideInInspector] public List<GameObject> bolts = new List<GameObject>();

    public SpriteRenderer SpriteRenderer;
    public PolygonCollider2D PolygonCollider;

    private void Awake()
    {
        GenerateHoldAndBoltsByWoodHold();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        PolygonCollider = GetComponent<PolygonCollider2D>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DeathZone"))
        {
            this.IsFallOver = true;

            // enable the polygon collider for improve performance
            this.gameObject.GetComponent<PolygonCollider2D>().enabled = false;

            // Disable after 0.5 second
            StartCoroutine(DisableAfterDelay(0.5f));
        }
    }

    private IEnumerator DisableAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (this.IsFallOver ) 
            gameObject.SetActive(false);
    }

    private void GenerateHoldAndBoltsByWoodHold()
    {
        if (boltsParent == null)
        {
            Debug.LogError("bolts parent gameobject is null");
            return;
        }

        // Get all transform in children of boltsParent, exclude the tranform of boltsParent
        Transform[] transforms = boltsParent.GetComponentsInChildren<Transform>()
                                   .Where(t => t != boltsParent.transform)
                                   .ToArray();

        foreach (Transform t in transforms)
        {
            Vector3 position = t.position;
            if (holdPrefab == null)
            {
                Debug.LogError("Hold prefab is null");
                return;
            }

            position = new Vector3(position.x, position.y, position.z);

            GameObject aHold = Instantiate(holdPrefab, position, Quaternion.identity);
            GameObject aBolt = Instantiate(boltPrefab, position, Quaternion.identity);
            holds.Add(aHold);
            bolts.Add(aBolt);
        }
    }

    private bool AreColorsSimilar(Color color1, Color color2, float tolerance = 0.01f)
    {
        return Mathf.Abs(color1.r - color2.r) < tolerance &&
               Mathf.Abs(color1.g - color2.g) < tolerance &&
               Mathf.Abs(color1.b - color2.b) < tolerance &&
               Mathf.Abs(color1.a - color2.a) < tolerance;
    }
}
