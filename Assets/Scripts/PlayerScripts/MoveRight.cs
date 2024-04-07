using UnityEngine;

public class MoveRight : IMovement
{
    private float speed = 5f;

    public void Move(GameObject gameObject)
    {
        gameObject.transform.Translate(speed * Time.deltaTime, 0, 0);
    }
}
