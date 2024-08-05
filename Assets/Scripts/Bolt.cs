using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt : MonoBehaviour
{
    [SerializeField] private Sprite selectedSpr;
    [SerializeField] private Sprite normalSpr;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SetAsSelected();
        } else if (Input.GetKeyDown(KeyCode.D))
        {
            SetAsNoSelected();
        }
    }

    public void SetAsSelected()
    {
        spriteRenderer.sprite = selectedSpr;
    }

    public void SetAsNoSelected()
    {
        spriteRenderer.sprite = normalSpr;
    }
}
