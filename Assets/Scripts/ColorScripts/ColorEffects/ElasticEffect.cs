using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElasticEffect : IColorEffect
{
    private Color effectColor;
    private ColorType colorType;
    private float minIntensity;

    public ElasticEffect(Color color, ColorType type, float intensity)
    {
        effectColor = color;
        colorType = type;
        minIntensity = intensity;
    }


    public void ApplyEffect(GameObject target)
    {
        Rigidbody2D rb = target.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(Vector2.up * minIntensity, ForceMode2D.Impulse);
        }
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
    

