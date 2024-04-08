using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElasticEffect : IColorEffect
{
    private Color effectColor;
    private ColorType colorType;
    private float minImpulse;

    public ElasticEffect(Color color, ColorType colorType, float minImpulse)
    {
        effectColor = color;
        this.colorType = colorType;
        this.minImpulse = minImpulse;
    }


    public void InitializeEffect(GameObject target)
    {
        target.GetComponent<SpriteRenderer>().color = effectColor;
        Debug.Log("APLICANDO EFECTO");
    }

    public void ApplyEffect()
    {
        throw new System.NotImplementedException();
    }

    public ColorType getColorType()
    {
        return colorType;
    }
}
    

