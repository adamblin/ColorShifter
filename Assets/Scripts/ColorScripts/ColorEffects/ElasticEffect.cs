using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElasticEffect : IColorEffect
{
    private Color effectColor;
    private float bounceIntensity;
    private ColorType colorType;

    public ElasticEffect(Color color, ColorType type, float intensity)
    {
        effectColor = color;
        colorType = type;
        bounceIntensity = intensity;
    }

    public void ApplyEffect(GameObject target)
    {

        //aplicar la logica del efecte
        target.GetComponent<SpriteRenderer>().color = effectColor;
        Debug.Log("APLICANDO EFECTO");
    }
    
   
}
    

