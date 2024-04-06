using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;


public class CharacterMovement: MonoBehaviour
{
    [SerializeField] private float playerSpeed = 5f;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }
}



//CODIGO ANTIGUO

//public float moveForce = 5f; // Fuerza de movimiento
//public float directionChangeSmoothness = 0.5f; // Suavidad en el cambio de dirección
//public float smoothBrakingFactor = 0.95f; // Factor de frenado suave, cerca pero menor a 1
//public Rigidbody2D rb; // Referencia al Rigidbody2D

//private Vector2 movement; // Vector de movimiento
//private Vector2 currentDirection = Vector2.zero; // Dirección actual de movimiento
//private bool facingRight = true; // Indica si el personaje está mirando hacia la derecha

//void Update()
//{
//    // Captura el input del jugador
//    movement.x = Input.GetAxisRaw("Horizontal");
//    movement.y = Input.GetAxisRaw("Vertical");

//    // Ajusta la dirección actual basándose en el input, para un cambio suave
//    if (movement != Vector2.zero)
//    {
//        currentDirection = Vector2.Lerp(currentDirection, movement, directionChangeSmoothness);

//        // Verifica si hay cambio de dirección
//        if ((movement.x > 0 && !facingRight) || (movement.x < 0 && facingRight))
//        {
//            // Cambia la dirección del personaje
//            Flip();
//        }
//    }
//}

//void FixedUpdate()
//{
//    if (movement != Vector2.zero)
//    {
//        // Aplica la fuerza de movimiento suavizada hacia la dirección actual
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
//    // Aplica un factor de frenado suave a la velocidad, reduciéndola gradualmente
//    rb.velocity *= smoothBrakingFactor;
//}

//void Flip()
//{
//    // Cambia la dirección del personaje multiplicando la escala en el eje X por -1
//    facingRight = !facingRight;
//    //Vector3 scale = transform.localScale;
//    //scale.x *= -1;
//    //transform.localScale = scale;

//    //cambiar rotacion del player
//    Vector3 currentRotation = transform.eulerAngles;
//    currentRotation.y += 180f;
//    transform.eulerAngles = currentRotation;
//}
