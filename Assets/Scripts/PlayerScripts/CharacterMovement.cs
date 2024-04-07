using UnityEditor.Timeline.Actions;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;


public class CharacterMovement: MonoBehaviour
{
    [SerializeField] private float playerSpeed = 5f;
    private bool facingRight = true;
    private bool canRotate = true;
    private Vector2 movementDirection;

    private Rigidbody2D rb;

    private IMovement moveRight;
    private IMovement moveLeft;
    private IMovement currentMovement;

    private Rigidbody2D rb;
    private bool facingRight = true;


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
        Debug.Log(canRotate);
        if (canRotate) {
            if ((movementDirection.x > 0 && !facingRight) || (movementDirection.x < 0 && facingRight))
            {
                facingRight = !facingRight;

                Vector3 currentRotation = transform.eulerAngles;
                currentRotation.y += 180f;
                transform.eulerAngles = currentRotation;
            }
        }
    }


    private void setCanRotate() { 
        canRotate = !canRotate;
    }

    private void OnEnable()
    {
        TongueController.onShootingTonge += setCanRotate;
    }

    private void OnDisable()
    {
        TongueController.onShootingTonge -= setCanRotate;
    }
    
}



//CODIGO ANTIGUO

//public float moveForce = 5f; // Fuerza de movimiento
//public float directionChangeSmoothness = 0.5f; // Suavidad en el cambio de direcci�n
//public float smoothBrakingFactor = 0.95f; // Factor de frenado suave, cerca pero menor a 1
//public Rigidbody2D rb; // Referencia al Rigidbody2D

//private Vector2 movement; // Vector de movimiento
//private Vector2 currentDirection = Vector2.zero; // Direcci�n actual de movimiento
//private bool facingRight = true; // Indica si el personaje est� mirando hacia la derecha

//void Update()
//{
//    // Captura el input del jugador
//    movement.x = Input.GetAxisRaw("Horizontal");
//    movement.y = Input.GetAxisRaw("Vertical");

//    // Ajusta la direcci�n actual bas�ndose en el input, para un cambio suave
//    if (movement != Vector2.zero)
//    {
//        currentDirection = Vector2.Lerp(currentDirection, movement, directionChangeSmoothness);

//        // Verifica si hay cambio de direcci�n
//        if ((movement.x > 0 && !facingRight) || (movement.x < 0 && facingRight))
//        {
//            // Cambia la direcci�n del personaje
//            Flip();
//        }
//    }
//}

//void FixedUpdate()
//{
//    if (movement != Vector2.zero)
//    {
//        // Aplica la fuerza de movimiento suavizada hacia la direcci�n actual
//        rb.AddForce(currentDirection.normalized * moveForce - rb.velocity * directionChangeSmoothness, ForceMode2D.Impulse);
//    }
//    else
//    {
//        // Suaviza el frenado reduciendo progresivamente la velocidad
//        SmoothBraking();
//    }
//}

//void SmoothBraking()
//{
//    // Aplica un factor de frenado suave a la velocidad, reduci�ndola gradualmente
//    rb.velocity *= smoothBrakingFactor;
//}

//void Flip()
//{
//    // Cambia la direcci�n del personaje multiplicando la escala en el eje X por -1
//    facingRight = !facingRight;
//    //Vector3 scale = transform.localScale;
//    //scale.x *= -1;
//    //transform.localScale = scale;

//    //cambiar rotacion del player
//    Vector3 currentRotation = transform.eulerAngles;
//    currentRotation.y += 180f;
//    transform.eulerAngles = currentRotation;
//}
