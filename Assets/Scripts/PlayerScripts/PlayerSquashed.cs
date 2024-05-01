using UnityEngine;
using System;

public class PlayerSquashed : MonoBehaviour
{
    [Header("CHECK TOP AND BOTTOM")]
    [SerializeField] private float topXSize;
    [SerializeField] private float topYSize;
    [SerializeField] private float topOffset;
    

    [SerializeField] private float bottomXSize;
    [SerializeField] private float bottomYSize;
    [SerializeField] private float bottomOffset;

    [Header("CHECK LATERALS")]
    [SerializeField] private float leftXSize;
    [SerializeField] private float leftYSize;
    [SerializeField] private float leftOffset;

    [SerializeField] private float rightXSize;
    [SerializeField] private float rightYSize;
    [SerializeField] private float rightOffset;

    [Header("WHAT CAN GET SQUASHED WITH")]
    [SerializeField] private LayerMask layerMask;


    private void Update()
    {
        CheckIfPlayerSquished();
    }

    private void CheckIfPlayerSquished() {
        Collider2D[] topCol = Physics2D.OverlapBoxAll(transform.position + new Vector3(0, transform.localScale.y + topOffset, 0), new Vector2(topXSize, topYSize), 0, layerMask);
        Collider2D[] botCol = Physics2D.OverlapBoxAll(transform.position - new Vector3(0, transform.localScale.y + bottomOffset, 0), new Vector2(topXSize, topYSize), 0, layerMask);
        if(topCol.Length != 0 && botCol.Length != 0)
            CheckIfKillPlayer(topCol[0], botCol[0]);

        Collider2D[] leftCol= Physics2D.OverlapBoxAll(transform.position - new Vector3(transform.localScale.x / 2 + leftOffset, 0, 0), new Vector2(leftXSize, leftYSize), 0, layerMask);
        Collider2D[] rightCol = Physics2D.OverlapBoxAll(transform.position + new Vector3(transform.localScale.x / 2 + rightOffset, 0, 0), new Vector2(topXSize, topYSize), 0, layerMask);
        if(leftCol.Length != 0 && rightCol.Length != 0)
            CheckIfKillPlayer(topCol[0], botCol[0]);

        Debug.Log("topCol: " + topCol.Length);
        Debug.Log("botCol: " + botCol.Length);
        Debug.Log("leftCol: " + leftCol.Length);
        Debug.Log("rightCol: " + rightCol.Length);
    }

    private void CheckIfKillPlayer(Collider2D col1, Collider2D col2) {
        Debug.Log("IN");
        
        

        //if(col1.gameObject.)
            //GameManager.Instance.MoveToCheckPoint();

        //if (collisions.Length >= 2) {
        //    int counter = 0;
        //    for (int i = 0; i < collisions.Length; i++){ //si estem chocant amb una paret y un obstacle de color groc
        //        GameObject obstacle = collisions[i].gameObject;

        //        if (obstacle.CompareTag("PaintableObstacle")) {
        //            ColorType obstacleColorType = obstacle.GetComponent<ObstacleEffectLogic>().getCurrentColorType();
        //            if (obstacleColorType == ColorType.Strech)
        //                counter++;
        //        }
        //        else
        //            counter++;
        //    }
        //    Debug.Log(counter);
        //    if (counter >= 2) {
        //        Debug.Log("Squashed");
        //        GameManager.Instance.MoveToCheckPoint();
        //    }
                
        //}
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + new Vector3(0, transform.localScale.y + topOffset, 0), new Vector2(topXSize, topYSize));
        Gizmos.DrawWireCube(transform.position - new Vector3(0, transform.localScale.y + bottomOffset, 0), new Vector2(bottomXSize, bottomYSize));
        Gizmos.DrawWireCube(transform.position - new Vector3(transform.localScale.x/2 + leftOffset, 0, 0), new Vector2(leftXSize, leftYSize));
        Gizmos.DrawWireCube(transform.position + new Vector3(transform.localScale.x/2 + rightOffset, 0, 0), new Vector2(rightXSize, rightYSize));
    }
}


