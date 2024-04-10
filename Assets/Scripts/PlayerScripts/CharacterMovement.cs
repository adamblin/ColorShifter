using UnityEditor;
using UnityEngine;


public class CharacterMovement: MonoBehaviour
{
    [Header("Moviment")]
    //PLAYER MOVEMENT
    [SerializeField] private float playerSpeed = 5f;
    [SerializeField] private float initialGravityScale;
    private bool facingRight = true;
    private bool canFlip = true;
    private Vector2 movementDirection;

    [Header("Jump")]
    //PLAYER JUMP
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float fallMultiplier;
    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGorunded;
    private Vector3 lastJumpPosition;
    Vector2 vecGravity;


    [Header("Water")] 
    private bool inWater = false;
    [SerializeField] private float speedInWater = 10f;
    [SerializeField] private float jumpForceInWater = 10f;
    [SerializeField] private float linearDrag = 10f;
    //[SerializeField] private float gravityInWater = 0.5f;







    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        vecGravity = new Vector2(0, -Physics2D.gravity.y);
    }

    private void FixedUpdate()
    {
        movementDirection.x = Input.GetAxisRaw("Horizontal");

        if(inWater)
            ApplyMovementInWater();
        else
            ApplyMovement();

        FlipPlayer();
        CheckJumpingLogic();
    }

    private void ApplyMovementInWater()
    {
        rb.drag = linearDrag;
        rb.gravityScale = 0;
        rb.AddForce(new Vector2(movementDirection.x * speedInWater * Time.deltaTime, 0));
    }

    private void ApplyMovement() {
        //rb.AddForce(new Vector2(movementDirection.x * playerSpeed * Time.deltaTime, 0));
        rb.velocity = new Vector2(movementDirection.x * playerSpeed, rb.velocity.y);
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

    private void CheckJumpingLogic() {
        //isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGorunded);

        Collider2D collider = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGorunded);

        if (collider == null)
        {
            isGrounded = false;
        }
        else {
            if (collider.gameObject.layer == 6 || collider.gameObject.layer == 7) //gorund layer
            {
                isGrounded = true;
                lastJumpPosition = transform.position;
            }
        }

        if (inWater) {
            //aplicar gravedad hacia abajo
        } 
        else
        {
            if (rb.velocity.y < 0)
            {
                rb.velocity -= vecGravity * fallMultiplier * Time.deltaTime;
            }
        }
    }

    private void PlayerJump() {

        if (isGrounded && !inWater)
        {
            //rb.AddForce(Vector2.up * jumpForce);
            rb.gravityScale = initialGravityScale;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        } 
        else if (inWater)
        {
            rb.AddForce(Vector2.up * jumpForceInWater);
        }
    }

    private void CanNotFlip() {
        canFlip = false;
    }

    private void CanFlip() {
        canFlip = true;
    }

    private void InWater() {
        inWater = !inWater;
    }


    private void OnEnable()
    {
        TongueController.onShootingTongue += CanNotFlip;
        TongueController.onNotMovingTongue += CanFlip;
        PlayerInputs.onJump += PlayerJump;
        WaterEffect.onWater += InWater;
    }

    private void OnDisable()
    {
        TongueController.onShootingTongue -= CanNotFlip;
        TongueController.onNotMovingTongue -= CanFlip;
        PlayerInputs.onJump -= PlayerJump;
        WaterEffect.onWater -= InWater;
    }

    public Vector3 getLastJumpPosition() { 
        return lastJumpPosition;
    }
}
