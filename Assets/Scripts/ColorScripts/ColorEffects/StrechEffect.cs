using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrechEffect : IColorEffect
{
    private Color effectColor;
    private ColorType colorType;

    public StrechEffect(Color color, ColorType type)
    {
        effectColor = color;
        colorType = type;
    }

    public void ApplyEffect(GameObject target)
    {
        throw new System.NotImplementedException();
    }

    public ColorType getColorType()
    {
        return colorType;
    }

    public void InitializeEffect(GameObject target)
    {
        //aplicar la logica del efecte
        target.GetComponent<SpriteRenderer>().color = effectColor;
        Debug.Log("APLICANDO EFECTO");
    }
}
