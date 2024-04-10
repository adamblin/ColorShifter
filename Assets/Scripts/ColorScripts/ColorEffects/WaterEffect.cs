using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class WaterEffect : IWaterEffect
{
    private Color effectColor;
    private ColorType colorType;

    private Color previousColor;

    public static event Action onWater;

    public WaterEffect(Color color, ColorType colorType) { 
        effectColor = color;
        this.colorType = colorType; 
    }

    public void InitializeEffect(GameObject target)
    {
        previousColor = target.GetComponent<SpriteRenderer>().color;
        target.GetComponent<SpriteRenderer>().color = effectColor;
        target.GetComponent<BoxCollider2D>().isTrigger = true;
    }

    public void ApplyEffect()
    {
        
    }

    public ColorType getColorType()
    {
        return colorType;
    }

    public void RemoveEffect(GameObject target)
    {
        target.GetComponent<SpriteRenderer>().color = previousColor;
        target.GetComponent<BoxCollider2D>().isTrigger = false;
    }
}
