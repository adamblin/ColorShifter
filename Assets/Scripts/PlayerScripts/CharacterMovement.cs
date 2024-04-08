using UnityEditor.Timeline.Actions;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float jumpForce = 7f;
    private Rigidbody2D rb;
    private float speed = 5f;
    private bool facingRight = true;
    public bool FacingRight => facingRight;
    private bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        
        if (moveHorizontal != 0)
        {
            Vector2 movement = new Vector2(moveHorizontal, 0);
            rb.velocity = new Vector2(movement.x * speed, rb.velocity.y);

            if ((moveHorizontal > 0 && !facingRight) || (moveHorizontal < 0 && facingRight))
            {
                Flip();
            }
        }

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void Flip()
    {
        Debug.Log("Volteando personaje.");
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    public void SetPosition(Vector2 newPosition)
    {
        rb.position = newPosition;
    }

    public void ResetToCheckpoint()
    {
        CheckpointSystem checkpointSystem = FindObjectOfType<CheckpointSystem>();
        if (checkpointSystem != null && checkpointSystem.CheckpointAvailable())
        {
            Vector3 checkpointPosition = checkpointSystem.GetCurrentCheckpointPosition();
            Debug.Log($"Moviendo al jugador al checkpoint en {checkpointPosition}.");
            transform.position = checkpointPosition;
        }
        else
        {
            Debug.LogError("No se encontró un sistema de checkpoint o no hay checkpoints disponibles.");
        }
    }

    public void PlayerDies()
    {
        Debug.Log("El jugador ha muerto, reiniciando en el último checkpoint.");
        ResetToCheckpoint();
    }

}

