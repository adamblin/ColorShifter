using UnityEditor.Timeline.Actions;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;


public class CharacterMovement: MonoBehaviour
{
   
    private IMovement moveRight;
    private IMovement moveLeft;
    private IMovement currentMovement;

    private Rigidbody2D rb;
    private bool facingRight = true;
    private bool canRotate = true;

    private void OnEnable()
    {
        PlayerInputs.onMoveRight += HandleMoveRight;
        PlayerInputs.onMoveLeft += HandleMoveLeft;
        TongueController.onShootingTonge += ToggleCanRotate;
    }

    private void OnDisable()
    {
        PlayerInputs.onMoveRight -= HandleMoveRight;
        PlayerInputs.onMoveLeft -= HandleMoveLeft;
        TongueController.onShootingTonge -= ToggleCanRotate;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveRight = new MoveRight();
        moveLeft = new MoveLeft();
    }

    private void Update()
    {
        if (currentMovement != null)
        {
            currentMovement.Move(gameObject);
            FlipPlayerIfNeeded();
        }
    }

    private void HandleMoveRight()
    {
        if (canRotate && !facingRight) FlipPlayer();
        currentMovement = moveRight;
    }

    private void HandleMoveLeft()
    {
        if (canRotate && facingRight) FlipPlayer();
        currentMovement = moveLeft;
    }

    private void FlipPlayer()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void ToggleCanRotate()
    {
        canRotate = !canRotate;
    }

    private void FlipPlayerIfNeeded()
    {
        bool shouldFaceRight = currentMovement == moveRight;
        if (shouldFaceRight != facingRight)
        {
            FlipPlayer();
        }
    }
}
