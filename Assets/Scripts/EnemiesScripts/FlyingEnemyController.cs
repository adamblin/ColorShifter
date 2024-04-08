using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyController : MonoBehaviour
{
    private IMovement movementStrategy;

    void Start()
    {
        movementStrategy = new HorizontalFlyMovement(true); 
    }

    void Update()
    {
        movementStrategy.Move(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Colisión detectada con: " + collision.gameObject.name);
        
        if(movementStrategy is HorizontalFlyMovement flyMovement)
        {
            flyMovement.ChangeDirection();
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("El jugador ha colisionado con el enemigo y debería 'morir'.");
            CharacterMovement playerMovement = collision.gameObject.GetComponent<CharacterMovement>();
            if (playerMovement != null)
            {
                playerMovement.PlayerDies();
            }
            else
            {
                Debug.Log("No se encontró el componente CharacterMovement en el jugador.");
            }
        }
    }
}
