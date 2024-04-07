using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovementController : MonoBehaviour
{
    private IMovement currentMovement;
    private bool movingRight = true;

    void Start()
    {
        currentMovement = new MoveRight();
    }

    void Update()
    {
        currentMovement.Move(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            movingRight = !movingRight;
            currentMovement = movingRight ? new MoveRight() : new MoveLeft();

            Flip();
        }
    }

    private void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}

