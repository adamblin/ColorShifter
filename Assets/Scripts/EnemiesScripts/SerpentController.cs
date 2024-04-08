using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerpentController : MonoBehaviour
{
    private SerpentMovement movementStrategy;

    void Start()
    {
        movementStrategy = new SerpentMovement(true);
    }

    void Update()
    {
        movementStrategy.Move(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        movementStrategy.ChangeDirection(gameObject);
    }
}
