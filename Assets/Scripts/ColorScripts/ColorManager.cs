using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    [Header("GENERAL")]
    [SerializeField] private Color waterColor;
    [SerializeField] private Color elasticColor;
    [SerializeField] private Color strechColor;

    private bool elasticAssigned = false;
    private bool waterAssigned = false;
    private bool strechAssigned = false;

    private IColorEffect DefaultObject;
    public static event Action<ColorType> onGetColorBack;

    //Elastic
    [Header("ELASTIC")]
    [SerializeField] private float elasticMinImpulse;
    [SerializeField] private float elasticHeightMultiplier;

    //Stretch
    [Header("STRECH")]
    [SerializeField] private float stretchMultiplier;
    [SerializeField] private LayerMask strechLayerMask;

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
                return new StrechEffect(strechColor, ColorType.Strech, stretchMultiplier, strechLayerMask);

            case ColorType.Default:
                if (strechAssigned)
                    onGetColorBack?.Invoke(ColorType.Strech);
                if (waterAssigned)
                    onGetColorBack?.Invoke(ColorType.Water);
                if (elasticAssigned)
                    onGetColorBack?.Invoke(ColorType.Elastic);
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
