using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    private Vector3 directionToMove;

    void Update()
    {
        MoveBullet();
    }

    private void MoveBullet() {
        if (directionToMove != null) {
            transform.position += directionToMove.normalized * bulletSpeed * Time.deltaTime;
        }
    }

    public void SetDirectionToMove(Vector3 direction){
        directionToMove = direction;    
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            FindAnyObjectByType<GameManager>().MoveToCheckPoint();

        Destroy(gameObject);
    }
}
