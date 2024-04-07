using UnityEngine;

public class JumpMovement : IMovement
{
    private float jumpForce = 7f;

    public void Move(GameObject gameObject)
    {
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        if (rb != null && IsGrounded(gameObject))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private bool IsGrounded(GameObject gameObject)
    {
        Vector2 position = gameObject.transform.position;
        Vector2 direction = Vector2.down;
        float distance = groundCheckDistance;

        RaycastHit2D hit = Physics2D.Raycast(position + groundCheckOffset, direction, distance, groundLayer);
        Debug.DrawRay(position + groundCheckOffset, direction * distance, Color.green);

        return hit.collider != null;
    }
}

