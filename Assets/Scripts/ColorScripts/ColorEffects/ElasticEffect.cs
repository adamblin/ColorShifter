using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElasticEffect : IColorEffect
{
    private Color effectColor;

    public ElasticEffect(Color color)
    {
        effectColor = color;
    }

    public void ApplyEffect(GameObject target)
    {
        //aplicar la logica del efecte
        target.GetComponent<SpriteRenderer>().color = effectColor;
        Debug.Log("APLICANDO EFECTO");
    }
}
    dvdsvv

