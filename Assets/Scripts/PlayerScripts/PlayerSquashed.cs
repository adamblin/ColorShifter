using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using JetBrains.Annotations;

public class PlayerSquashed : MonoBehaviour
{
    [Header("CHECK TOP AND BOTTOM")]
    [SerializeField] private float topXSize;
    [SerializeField] private float topYSize;

    [Header("CHECK LATERALS")]
    [SerializeField] private float latXSize;
    [SerializeField] private float latYSize;

    [Header("WHAT CAN GET SQUASHED WITH")]
    [SerializeField] private LayerMask layerMask;


    private void Update()
    {
        CheckIfPlayerSquished();
    }

    private void CheckIfPlayerSquished() {
        Collider2D[] topAndBottomCol = Physics2D.OverlapBoxAll(transform.position, new Vector2(topXSize, topYSize), 0, layerMask);
        CheckIfKillPlayer(topAndBottomCol);

        Collider2D[] lateralCol = Physics2D.OverlapBoxAll(transform.position, new Vector2(latXSize, latYSize), 0, layerMask);
        CheckIfKillPlayer(lateralCol);

        Debug.Log("top: " + topAndBottomCol.Length + " lateral: " + lateralCol.Length);
    }

    private void CheckIfKillPlayer(Collider2D[] collisions) {
        if (collisions.Length >= 2) {
            for (int i = 0; i < collisions.Length; i++){ //si estem chocant amb una paret y un obstacle de color groc
                GameObject obstacle = collisions[i].gameObject;

                if (obstacle.CompareTag("PaintableObstacle"))
                    if(obstacle.GetComponent<ObstacleEffectLogic>().getCurrentColorType() == ColorType.Strech)
                        GameManager.Instance.MoveToCheckPoint();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector2(topXSize, topYSize));
        Gizmos.DrawWireCube(transform.position, new Vector2(latXSize, latYSize));
    }
}


