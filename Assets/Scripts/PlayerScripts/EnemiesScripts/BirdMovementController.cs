using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMovementController : MonoBehaviour
{
    private IMovement movementStrategy;
    private bool facingRight = true;

    private void Start()
    {
        movementStrategy = new MoveHorizontal();
    }

    private void Update()
    {
        movementStrategy.Execute(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        facingRight = !facingRight;

        Vector3 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        gameObject.transform.localScale = localScale;

        movementStrategy = new MoveHorizontal();
    }
}
