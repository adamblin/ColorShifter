using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterJump : MonoBehaviour
{
    public float jumpForce = 5f; // Fuerza del salto
    public Transform groundCheck; // Punto para verificar si el personaje está en el suelo
    public float groundCheckRadius = 0.2f; // Radio del chequeo de suelo
    public LayerMask groundLayer; // Capa que representa el suelo
    public float fallMultiplier = 2.5f; // Multiplicador de la gravedad al caer

    private Rigidbody2D rb;
    private bool isGrounded; // Indica si el personaje está tocando el suelo
    private bool canJump; // Indica si el personaje puede saltar

    public float maxJumpHeight = 2f; // Altura máxima del salto
    private float jumpStartHeight; // Altura al iniciar el salto


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        canJump = true; // Inicialmente el personaje puede saltar
    }

    void Update()
    {
        // Verifica si el personaje está en el suelo
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded && !canJump)
        {
            canJump = true; // Permite saltar nuevamente una vez que toca el suelo
        }

        if (Input.GetButtonDown("Jump") && isGrounded && canJump)
        {
            // Aplica la fuerza de salto
            Jump();
        }
    }

    void FixedUpdate()
    {
        if (rb.velocity.y < 0) // El personaje está cayendo
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump")) // Ascenso sin presionar el botón de salto
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }

        // Verifica si el personaje ha alcanzado la altura máxima del salto
        if (transform.position.y - jumpStartHeight >= maxJumpHeight)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0); // Detiene el movimiento ascendente
        }
    }


    void Jump()
    {
        jumpStartHeight = transform.position.y; // Almacena la altura al iniciar el salto
        rb.velocity = new Vector2(rb.velocity.x, 0f); // Restablece la velocidad vertical
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); // Aplica la fuerza de salto
        canJump = false; // Impide saltar nuevamente hasta tocar el suelo
    }


    void OnDrawGizmos()
    {
        // Dibuja el chequeo de suelo en el editor para facilitar la configuración
        if (groundCheck != null)
        {
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}

