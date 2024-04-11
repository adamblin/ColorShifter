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
        onWater?.Invoke();
    }

    public ColorType getColorType()
    {
        return colorType;
    }

    public void RemoveEffect(GameObject target)
    {
        Collider2D[] collider = Physics2D.OverlapBoxAll(target.transform.position, target.transform.localScale, 0f);

        for (int i = 0; i < collider.Length; i++) {
            if (collider[i].gameObject.CompareTag("Player")) {
                onWater?.Invoke();
                Debug.Log("PLAYER IN ");
            }
        }
        target.GetComponent<SpriteRenderer>().color = previousColor;
        target.GetComponent<BoxCollider2D>().isTrigger = false;
    }
}
