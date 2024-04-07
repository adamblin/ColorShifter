using UnityEngine;

public class MoveRight : IMovement
{
    private float speed = 5f;

    public void Move(GameObject gameObject)
    {
        gameObject.transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
}
