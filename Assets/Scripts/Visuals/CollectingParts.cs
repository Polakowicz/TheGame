using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectingParts : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.CompareTag("Player"))
        {
            var spriteRendererColor = _spriteRenderer.color;
            spriteRendererColor.a = 0;
            _spriteRenderer.color = spriteRendererColor;
        }
    }
}
