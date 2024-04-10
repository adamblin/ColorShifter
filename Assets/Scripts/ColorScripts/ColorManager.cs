using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    [SerializeField] private Color waterColor;
    [SerializeField] private Color elasticColor;
    [SerializeField] private Color strechColor;

    private bool elasticAssigned = false;
    private bool waterAssigned = false;
    private bool strechAssigned = false;

    private IColorEffect DefaultObject;

    //Elastic
    [SerializeField] private float elasticMinImpulse;
    [SerializeField] private float elasticHeightMultiplier;

    //Stretch
    [SerializeField] private float stretchMultiplier;

    private void Start()
    {
        DefaultObject = new DefaultEffect(Color.white, ColorType.Default);
    }

    public IColorEffect GetColorEffect(ColorType colorType)
    {
        switch (colorType){

            case ColorType.Elastic:
                if (elasticAssigned)
                    return DefaultObject;
                elasticAssigned = true;
                return new ElasticEffect(elasticColor, ColorType.Elastic, elasticMinImpulse, elasticHeightMultiplier);

            case ColorType.Water:
                if (waterAssigned)
                    return DefaultObject;
                waterAssigned = true;
                return new WaterEffect(waterColor, ColorType.Water);

            case ColorType.Strech:
                if (strechAssigned)
                    return DefaultObject;
                strechAssigned = true;
                return new StrechEffect(strechColor, ColorType.Strech,stretchMultiplier);

            case ColorType.Default:
                //return null;
                return DefaultObject;

            default:
                throw new ArgumentException("Color no soportado", nameof(colorType));
        }
    }

    public Color GetColor(ColorType colorType) {
        switch (colorType)
        {

            case ColorType.Elastic:
                if(elasticAssigned)
                    return Color.white;
                return elasticColor;

            case ColorType.Water:
                if (waterAssigned)
                    return Color.white;
                return waterColor;

            case ColorType.Strech:
                if (strechAssigned)
                    return Color.white;
                return strechColor;

            case ColorType.Default:
                return Color.white;

            default:
                throw new ArgumentException("Color no soportado", nameof(colorType));
        }
    }

    private void ChangeAssigneds(ColorType colorType)
    {
        switch (colorType) { 
            case ColorType.Elastic:
                elasticAssigned = false;
                break;
            case ColorType.Water:
                waterAssigned = false;
                break;
            case ColorType.Strech:
                strechAssigned = false;
                break;
            default:
                break;

        }
    }

    private void OnEnable()
    {
        ObstacleEffectLogic.onChangeEffect += ChangeAssigneds;
    }

    private void OnDisable()
    {
        ObstacleEffectLogic.onChangeEffect -= ChangeAssigneds;
    }
}
