using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElasticEffect : IColorEffect
{
    private Color effectColor;
    private ColorType colorType;
    private float minImpulse;

    private Color previousColor;

    public ElasticEffect(Color color, ColorType colorType, float minImpulse)
    {
        effectColor = color;
        this.colorType = colorType;
        this.minImpulse = minImpulse;
    }


    public void InitializeEffect(GameObject target)
    {
        previousColor = target.GetComponent<SpriteRenderer>().color;
        target.GetComponent<SpriteRenderer>().color = effectColor;
        Debug.Log("APLICANDO EFECTO");
    }

    public void ApplyEffect(GameObject target)
    {
        Rigidbody2D rb = target.GetComponent<Rigidbody2D>();
        Debug.Log(target);
        

    }

    public IColorEffect RemoveEffect(GameObject target)
    {
        target.GetComponent<SpriteRenderer>().color = previousColor;
        return null;
    }

    public ColorType getColorType()
    {
        return colorType;
    }

    
}
    

