using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WoodPuzzle.Core;

public class Player : MonoBehaviour
{
    private Bolt holdingBolt = null;

    private void Update()
    {
        ProcessUserClicking();
    }

    private void ProcessUserClicking()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit;

            LayerMask boltLayer = LayerMask.GetMask("Bolt_UI");
            LayerMask holdLayer = LayerMask.GetMask("Hold_UI");

            hit = Physics2D.Raycast(ray, Vector2.zero, Mathf.Infinity, boltLayer);

            if (this.holdingBolt != null && hit.collider == null)
            {
                hit = Physics2D.Raycast(ray, Vector2.zero, Mathf.Infinity, holdLayer);
            } 


            if (hit.collider != null)
            {
                Debug.Log(hit.collider.gameObject.name);

                // Check if picking the bolt
                Bolt clickedBolt = hit.collider.GetComponentInParent<Bolt>();
                if (clickedBolt != null)
                {
                    UpdateHoldingBolt(clickedBolt);
                }

                // Check if click to a hold
                Hold clickedHold = hit.collider.GetComponentInParent<Hold>();
                if (clickedHold != null)
                {
                    Vector3 holdPosition = clickedHold.gameObject.transform.position;
                    holdPosition = new Vector3(holdPosition.x, holdPosition.y, holdPosition.z);
                    if (holdingBolt != null)
                    {
                        if (!IsHoldOverlapseWithWood(clickedHold))
                        {
                            UpdateHoldingBoltPosition(holdPosition);
                        }
                        else
                        {
                            AudioManager.Instance.PlaySFX("BoltInHoldFail");
                            Debug.Log("Collide with " + hit.collider.gameObject.name + "| hold overlapse with wood");
                        }
                    }
                }
            }
        }
    }

    private bool IsHoldOverlapseWithWood(Hold clickedHold)
    {
        CircleCollider2D holdCollider = clickedHold.GetComponent<CircleCollider2D>();
        Collider2D[] overlappingColliders = Physics2D.OverlapCircleAll(holdCollider.bounds.center, holdCollider.radius);

        foreach (Collider2D collider in overlappingColliders)
        {
            if (collider.GetComponent<Wood>() != null)
            {
                return true;
            }
        }

        return false;   
    }

    private void UpdateHoldingBolt(Bolt clickedBolt)
    {
        // If the clicked bolt is current holding bolt, just de-select
        if (clickedBolt == holdingBolt)
        {
            holdingBolt.SetAsNoSelected();
            holdingBolt = null;
        }
        else
        {
            // Set the prev holding bolt is not selected anymore
            if (holdingBolt != null)
            {
                holdingBolt.SetAsNoSelected();
            }

            // setup new holding bolt and set it is selected
            holdingBolt = clickedBolt;
            holdingBolt.SetAsSelected();

            AudioManager.Instance.PlaySFX("ChooseABolt");
        }
    }

    private void UpdateHoldingBoltPosition(Vector3 position)
    {
        if (holdingBolt == null)
        {
            Debug.LogError("Holding bolt is null");
            return;
        }

        AudioManager.Instance.PlaySFX("BoltInHold");

        holdingBolt.transform.position = position;
        holdingBolt.SetAsNoSelected();
        holdingBolt = null;
    }
}
