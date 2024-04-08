using UnityEditor.Timeline.Actions;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;


public class CharacterMovement: MonoBehaviour
{
    [SerializeField] private float playerSpeed = 5f;
    private bool facingRight = true;
    private bool canFlip = true;
    private Vector2 movementDirection;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        movementDirection.x = Input.GetAxisRaw("Horizontal");
        ApplyMovement();
        FlipPlayer();
    }

    private void ApplyMovement() {
        rb.velocity = new Vector2(movementDirection.x * playerSpeed, 0);
    }

    private void FlipPlayer(){
        if (canFlip) {
            if ((movementDirection.x > 0 && !facingRight) || (movementDirection.x < 0 && facingRight))
            {
                facingRight = !facingRight;

                Vector3 currentRotation = transform.eulerAngles;
                currentRotation.y += 180f;
                transform.eulerAngles = currentRotation;
            }
        }
    }

    private void CanNotFlip() {
        canFlip = false;
    }

    private void CanFlip() {
        canFlip = true;
    }


    private void OnEnable()
    {
        TongueController.onShootingTongue += CanNotFlip;
        TongueController.onNotMovingTongue += CanFlip;
    }

    private void OnDisable()
    {
        TongueController.onShootingTongue -= CanNotFlip;
        TongueController.onNotMovingTongue -= CanFlip;
    }
}
