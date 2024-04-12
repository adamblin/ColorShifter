using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CheckPoint : MonoBehaviour
{
    public static event Action<GameObject> onCheckPoint;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.red;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            spriteRenderer.color = Color.green;
            onCheckPoint?.Invoke(gameObject);
            Destroy(GetComponent<Collider2D>());
        } 
    }
}
