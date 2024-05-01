using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultEffect : IColorEffect
{
    private Color effectColor;
    private ColorType colorType;

    public DefaultEffect(Color color, ColorType colorType) { 
        effectColor = color;
        this.colorType = colorType;
    }

    public ColorType getColorType()
    {
        return colorType;
    }

    public void InitializeEffect(GameObject target)
    {
        target.GetComponent<SpriteRenderer>().color = effectColor;
    }

    public void RemoveEffect(GameObject target)
    {

    }
}
