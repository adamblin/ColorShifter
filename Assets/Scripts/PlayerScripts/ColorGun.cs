using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

using UnityEngine;

public class ColorGun : MonoBehaviour
{
    // Array de los tipos de color disponibles para disparar.
    private ColorType[] colorTypes;
    private int currentColorIndex = 0;

    // Referencia a ColorManager para solicitar instancias de las propiedades de color
    private ColorManager colorManager;

    // Atributos para el gizmo
    private Vector2 lastRaycastDirection = Vector2.zero;
    private Vector2 lastRaycastStart = Vector2.zero;


    void Start()
    {
        colorTypes = new ColorType[]
        {
            ColorType.Elastic,
            ColorType.Water,
            ColorType.Strech
            // Añadir más tipos según sea necesario
        };

        colorManager = FindObjectOfType<ColorManager>(); // Encuentra la instancia de ColorManager en la escena
    }

    void Update()
    {
        // Detectar la pulsación de la tecla para rotar los colores
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RotateColors();
        }

        // Detectar la pulsación para disparar
        if (Input.GetKeyDown(KeyCode.G))
        {
            ShootCurrentColor();
        }
    }

    private void RotateColors()
    {
        currentColorIndex = (currentColorIndex + 1) % colorTypes.Length;
        // Opcional: Mostrar visualmente el cambio de color actual
    }

    private void ShootCurrentColor()
    {
       IColorEffect currentProperty = colorManager.GetColorEffect(colorTypes[currentColorIndex]);
        ShootColor(currentProperty);
        
    }

    private void ShootColor(IColorEffect colorEffect) // Cambio de IColorProperty a IColorEffect
    {
        // Para juegos 2D, es posible que necesites ajustar esta dirección.
        Vector2 direction = transform.right; // Asumiendo que "derecha" es la dirección en la que quieres disparar
        lastRaycastStart = transform.position;
        lastRaycastDirection = direction * 10; // Ajusta la longitud del raycast según sea necesario

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction);
        if (hit.collider != null)
        {
            ObstacleEffectLogic colorableObject = hit.collider.gameObject.GetComponent<ObstacleEffectLogic>();
            if (colorableObject != null)
            {
                colorableObject.ApplyEffect(colorEffect); // Asume que este método acepta IColorEffect
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        // Dibuja una línea desde el inicio del raycast en la dirección y longitud almacenadas
        if (lastRaycastDirection != Vector2.zero)
        {
            Gizmos.DrawLine(lastRaycastStart, lastRaycastStart + lastRaycastDirection);
        }
    }

}