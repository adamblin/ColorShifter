using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyController : MonoBehaviour
{
    private IMovement movementStrategy;

    void Start()
    {
        movementStrategy = new HorizontalFlyMovement(true); 
    }

    void Update()
    {
        movementStrategy.Move(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(movementStrategy is HorizontalFlyMovement flyMovement)
        {
            flyMovement.ChangeDirection();
        }
    }
}
