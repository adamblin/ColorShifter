using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    [SerializeField] private Color waterColor;
    [SerializeField] private Color elasticColor;
    [SerializeField] private Color strechColor;

    public IColorEffect GetColorEffect(ColorType colorType)
    {
        switch (colorType){

            case ColorType.Elastic:
                return new ElasticEffect(elasticColor);

            case ColorType.Water:
                return new WaterEffect(waterColor);

            case ColorType.Strech:
                return new StrechEffect(strechColor);

            default:
                throw new ArgumentException("Color no soportado", nameof(colorType));
        }
    }
}
