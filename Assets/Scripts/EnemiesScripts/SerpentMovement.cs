using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerpentMovement : IMovement
{
    private float speed = 3f;
    public bool MovingRight { get; private set; } = true;

    public SerpentMovement(bool initialDirectionRight)
    {
        MovingRight = initialDirectionRight;
        speed = MovingRight ? Mathf.Abs(speed) : -Mathf.Abs(speed);
    }

    public void Move(GameObject gameObject)
    {
        gameObject.transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    public void ChangeDirection(GameObject gameObject)
    {
        MovingRight = !MovingRight;
        speed = -speed;

        Vector3 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        gameObject.transform.localScale = localScale;
    }
}

