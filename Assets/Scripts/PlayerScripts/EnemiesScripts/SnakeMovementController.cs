using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovementController : MonoBehaviour
{
    void Start()
    {
        currentMovement = new MoveRight();
    }

    void Update()
    {
        currentMovement.Execute(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            if (currentMovement is MoveRight)
            {
                currentMovement = new MoveLeft();
            }
            else if (currentMovement is MoveLeft)
            {
                currentMovement = new MoveRight();
            }
        }
    }
}
