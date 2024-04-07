using UnityEngine;

public class MoveLeft : IMovement
{
     private float speed = 5f;

    public void Move(GameObject gameObject)
    {
        gameObject.transform.Translate(Vector2.left * speed * Time.deltaTime);
    }
}
