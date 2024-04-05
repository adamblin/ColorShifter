using System.Collections;
using System.Collections.Generic;
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
        currentColorEffect.ApplyEffect(gameObject);
    }
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
