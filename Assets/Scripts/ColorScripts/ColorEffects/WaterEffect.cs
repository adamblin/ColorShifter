using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterEffect : IWaterEffect
{
    private Color effectColor;
    private ColorType colorType;

    private Color previousColor;

    public WaterEffect(Color color, ColorType colorType) { 
        effectColor = color;
        this.colorType = colorType; 
    }



    public void InitializeEffect(GameObject target)
    {
        previousColor = target.GetComponent<SpriteRenderer>().color;
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

    public void RemoveEffect(GameObject target)
    {
        target.GetComponent<SpriteRenderer>().color = previousColor;
    }
}
