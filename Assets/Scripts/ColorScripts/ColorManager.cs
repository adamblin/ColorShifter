using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    [SerializeField] private Color waterColor;
    [SerializeField] private Color elasticColor;
    [SerializeField] private Color strechColor;

    //Atributos de Objetos
    [SerializeField] private float minIntensity;

    public IColorEffect GetColorEffect(ColorType colorType)
    {
        switch (colorType){

            case ColorType.Elastic:
                return new ElasticEffect(elasticColor,ColorType.Elastic,minIntensity);

            case ColorType.Water:
                return new WaterEffect(waterColor,ColorType.Water);

            case ColorType.Strech:
                return new StrechEffect(strechColor,ColorType.Strech);

            default:
                throw new ArgumentException("Color no soportado", nameof(colorType));
        }
    }
}
