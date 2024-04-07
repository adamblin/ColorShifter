using UnityEngine;

public class MoveLeft : IMovement
{
    private float speed = 5f;

    public void Move(GameObject gameObject)
    {
        gameObject.transform.Translate(-speed * Time.deltaTime, 0, 0);
    }
}
