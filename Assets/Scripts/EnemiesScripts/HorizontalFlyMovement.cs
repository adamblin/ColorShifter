using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalFlyMovement : IMovement
{
    private float speed = 3f;
    public bool MovingRight { get; private set; } = true;

    public HorizontalFlyMovement(bool initialDirectionRight)
    {
        MovingRight = initialDirectionRight;
        speed = MovingRight ? Mathf.Abs(speed) : -Mathf.Abs(speed);
    }

    public void Move(GameObject gameObject)
    {
        gameObject.transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    public void ChangeDirection()
    {
        MovingRight = !MovingRight;
        speed = -speed;
    }
}

