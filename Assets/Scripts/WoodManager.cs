using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class WoodManager : MonoBehaviour
{
    //Singleton
    public static WoodManager Instance { get; private set; }

    private Wood[] woodList;
    [SerializeField] private GameObject holdsParentInScene;
    [SerializeField] private GameObject boltsParentInScene;

    private void Awake()
    {
        // Setup singleton
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
        }

        woodList = FindObjectsOfType<Wood>();

        ActiveTheWoodCollider(false);
        IgnoreColliderForAllWood();
        ActiveTheWoodCollider(true);
    }

    private void Start()
    {
        DestroyDuplicatedHold();
        DestroyDuplicatedBolt();
    }

    private void Update()
    {
       
    }

    private void IgnoreColliderForAllWood()
    {
        foreach (var w1 in woodList)
        {
            foreach (var w2 in woodList)
            {
                if (w1 == w2) continue;

                Physics2D.IgnoreCollision(w1.PolygonCollider, w2.PolygonCollider, true);
            }
        }
    }

    public bool AllWoodIsFallover()
    {
        foreach (var w in woodList)
        {
            if (!w.IsFallOver) return false; 
        }

        return true;
    }

    private void DestroyDuplicatedHold()
    {
        List<GameObject> holds = new List<GameObject>();
        foreach (var w in woodList)
        {
            holds.AddRange(w.holds); // Add all elements from w.holds to holds
        }

        float minimumDistance = 2f;
        List<GameObject> toRemove = new List<GameObject>();

        for (int i = 0; i < holds.Count; i++)
        {
            for (int j = i + 1; j < holds.Count; j++)
            {
                float distance = Vector3.Distance(holds[i].transform.position, holds[j].transform.position);
                if (distance < minimumDistance)
                {
                    toRemove.Add(holds[i]);
                    break; // No need to check other elements for the current hold[i]
                }
            }
        }

        foreach (var item in toRemove)
        {
            holds.Remove(item);
            Destroy(item);
        }

        foreach (var item in holds)
        {
            item.gameObject.transform.parent = holdsParentInScene.transform;
        }
    }

    private void DestroyDuplicatedBolt()
    {
        List<GameObject> bolts = new List<GameObject>();
        foreach (var w in woodList)
        {
            bolts.AddRange(w.bolts); 
        }

        float minimumDistance = 2f;
        List<GameObject> toRemove = new List<GameObject>();

        for (int i = 0; i < bolts.Count; i++)
        {
            for (int j = i + 1; j < bolts.Count; j++)
            {
                float distance = Vector3.Distance(bolts[i].transform.position, bolts[j].transform.position);
                if (distance < minimumDistance)
                {
                    toRemove.Add(bolts[i]);
                    break; // No need to check other elements for the current hold[i]
                }
            }
        }

        foreach (var item in toRemove)
        {
            bolts.Remove(item);
            Destroy(item);
        }

        foreach (var item in bolts)
        {
            item.gameObject.transform.parent = boltsParentInScene.transform;
        }
    }

    private void ActiveTheWoodCollider(bool isActive = true)
    {
        foreach (var w in woodList)
        {
            w.PolygonCollider.enabled = isActive;
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
