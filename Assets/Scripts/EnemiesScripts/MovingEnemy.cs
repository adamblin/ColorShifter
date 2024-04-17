using UnityEngine;

public class MovingEnemy : MonoBehaviour
{
    [SerializeField] private EnemyType enemyType;
    [SerializeField] private float enemySpeed;
    [SerializeField] private Transform pointA, pointB;

    private bool gotToPointA = false; 
    private Vector3 finalPositionA, finalPositionB;

    

    private void Start()
    {
        if (enemyType == EnemyType.Snake)
        {
            pointA.position = new Vector2(pointA.position.x, transform.position.y);
            pointB.position = new Vector2(pointB.position.x, transform.position.y);
        }
        else 
        {
            pointA.position = new Vector2(transform.position.x, pointA.position.y);
            pointB.position = new Vector2(transform.position.x, pointB.position.y);
        }

        finalPositionA = pointA.position;
        finalPositionB = pointB.position;
    }

    private void FixedUpdate()
    {
        Vector3 direction = GetDirection();
        if (Vector2.Distance(transform.position, direction) != 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, direction, enemySpeed * Time.deltaTime);
        }
        else
        {
            gotToPointA = !gotToPointA;
        }
    }

    

    private Vector3 GetDirection() {
        if (!gotToPointA)
            return finalPositionA;
        else
            return finalPositionB;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            FindAnyObjectByType<GameManager>().MoveToCheckPoint();
        }
    }
}
