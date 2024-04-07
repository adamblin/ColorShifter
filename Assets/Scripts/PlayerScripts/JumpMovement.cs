using UnityEngine;

public class JumpMovement : IMovement
{
    private float jumpForce = 7f;
    private float groundCheckDistance = 0.1f;
    private Vector2 groundCheckOffset = new Vector2(0, -0.5f);
    private LayerMask groundLayer;

    public JumpMovement(LayerMask groundLayer)
    {
        this.groundLayer = groundLayer;
    }

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

