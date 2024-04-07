using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterEffect : IColorEffect
{
    private Color effectColor;
    private ColorType colorType;

    public WaterEffect(Color color, ColorType type) { 
        effectColor = color;
        colorType = type;
    }

    public void ApplyEffect(GameObject target)
    {
        throw new System.NotImplementedException();
    }

    

    public void InitializeEffect(GameObject target)
    {
        //aplicar la logica del efecte
        target.GetComponent<SpriteRenderer>().color = effectColor;
        Debug.Log("APLICANDO EFECTO");
    }

    public ColorType getColorType()
    {
        return colorType;
    }
}
