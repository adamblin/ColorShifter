using Microsoft.Unity.VisualStudio.Editor;
using UnityEditor;
using UnityEngine;


public class CharacterMovement: MonoBehaviour
{
    [Header("MOVEMENT")]
    //PLAYER MOVEMENT
    [SerializeField] private float playerSpeed = 5f;
    [SerializeField] private float initialGravityScale;
    private bool facingRight = true;
    private bool canFlip = true;
    private Vector2 movementDirection;

    [Header("JUMP")]
    //PLAYER JUMP
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float fallMultiplier;
    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGorunded;
    private Vector3 lastJumpPosition;
    Vector2 vecGravity;

    [Header("UNDER WATER")] 
    [SerializeField] private float speedInWater = 10f;
    [SerializeField] private float jumpForceInWater = 10f;
    [SerializeField] private float linearDrag = 10f;
    [SerializeField] private float gravityInWater = 0.5f;
    private bool inWater = false;

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

        FlipPlayerByMovement();
        CheckJumpingLogic();
    }

    private void ApplyMovementInWater()
    {
        rb.drag = linearDrag;
        rb.gravityScale = gravityInWater;
        //falta usar el Time.deltaTime
        rb.AddForce(new Vector2(movementDirection.x * speedInWater , 0));
    }

    private void ApplyMovement() {
        rb.gravityScale = initialGravityScale;
        rb.drag = 0;
        //falta usar el Time.deltaTime
        rb.velocity = new Vector2(movementDirection.x * playerSpeed , rb.velocity.y);
    }

    private void FlipPlayerByMovement(){
        
        if (canFlip) {
            if ((movementDirection.x > 0 && !facingRight) || (movementDirection.x < 0 && facingRight))
                RotatePlayer();
        }
    }

    private void CheckIfLookingRightDirectionOnShoot(Vector3 shootDirection) {
        if (shootDirection.x < 0 && facingRight)
            RotatePlayer();
        else if(shootDirection.x >= 0 && !facingRight)
            RotatePlayer();
    }

    private void RotatePlayer() {
        facingRight = !facingRight;

        //Vector3 currentRotation = transform.eulerAngles;
        //currentRotation.y += 180f;
        //transform.eulerAngles = currentRotation;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void CheckJumpingLogic() {
        Collider2D collider = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGorunded);

        if (collider == null)
        {
            isGrounded = false;
        }
        else {
            if (collider.gameObject.layer == 6) //gorund layer
            {
                isGrounded = true;
                lastJumpPosition = transform.position;
            }
            else if (collider.gameObject.layer == 7) //obstacles
            { 
                isGrounded = true;
                if (collider.gameObject.GetComponent<ObstacleEffectLogic>().getCurrentColorType() != ColorType.Elastic)
                    lastJumpPosition = transform.position;
            }
        }

        if (inWater) {
            rb.AddForce(-vecGravity * fallMultiplier * Time.deltaTime);
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
        TongueController.Instance.onShootingTongue += CanNotFlip;
        TongueController.Instance.onNotMovingTongue += CanFlip;
        TongueController.Instance.shootDirection += CheckIfLookingRightDirectionOnShoot;
        PlayerInputs.onJump += PlayerJump;
        WaterEffect.onWater += InWater;
    }

    private void OnDisable()
    {
        TongueController.Instance.onShootingTongue -= CanNotFlip;
        TongueController.Instance.onNotMovingTongue -= CanFlip;
        TongueController.Instance.shootDirection -= CheckIfLookingRightDirectionOnShoot;
        PlayerInputs.onJump -= PlayerJump;
        WaterEffect.onWater -= InWater;
    }

    public Vector3 getLastJumpPosition() { 
        return lastJumpPosition;
    }
}
