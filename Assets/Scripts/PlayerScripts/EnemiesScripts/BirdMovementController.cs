using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMovementController : MonoBehaviour
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
        movingRight = !movingRight;
        currentMovement = movingRight ? new MoveRight() : new MoveLeft();

        FlipSprite();
    }

    private void FlipSprite()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}

