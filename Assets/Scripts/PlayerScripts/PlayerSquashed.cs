using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerSquashed : MonoBehaviour
{
    [SerializeField] private float xSize, ySize;
    [SerializeField] private LayerMask layerMask;


    private void Update()
    {
        CheckIfPlayerSquished();
    }

    private void CheckIfPlayerSquished() {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(xSize, ySize), 0, layerMask);
        if (colliders.Length == 2) { 
            FindAnyObjectByType<GameManager>().MoveToCheckPoint();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, new Vector2(xSize, ySize));
    }
}


