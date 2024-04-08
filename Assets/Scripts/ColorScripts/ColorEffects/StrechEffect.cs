using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrechEffect : IColorEffect
{
    private Color effectColor;
    private ColorType colorType;

    public StrechEffect(Color color, ColorType colorType)
    {
        effectColor = color;
        this.colorType = colorType;
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
