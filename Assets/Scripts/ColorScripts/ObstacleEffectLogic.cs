using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class ObstacleEffectLogic : MonoBehaviour
{
    private IColorEffect currentColorEffect;

    public void ApplyEffect(IColorEffect colorEffect) {
        if (currentColorEffect != null) { 
            //funcion para limpiar el color actual, ejemplo: currentColorEffect.RemoveEffect();
        }

        currentColorEffect = colorEffect;
        currentColorEffect.InitializeEffect(gameObject);
    }
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (currentColorEffect.getColorType() == ColorType.Elastic)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                currentColorEffect.ApplyEffect(collision.gameObject);
            }
        }
    }
}
