using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.Rendering;
using System;

public class ObstacleEffectLogic : MonoBehaviour
{
    private IColorEffect currentColorEffect;
    private ColorType currentColorType;

    public static event Action<ColorType> onChangeEffect;

    //LOGICA STRECH
    private IStrechEffect lastStrechEffect;
    private Vector3 initialScale;

    public void ApplyEffect(IColorEffect colorEffect) {

        if (currentColorType == ColorType.Default)
        {
            currentColorEffect = colorEffect;
            currentColorEffect.InitializeEffect(gameObject);
            currentColorType = currentColorEffect.getColorType();
            lastStrechEffect = null;

        } else {
            if (currentColorType == ColorType.Strech) {
                IStrechEffect effect = currentColorEffect as IStrechEffect;
                lastStrechEffect = effect;
            }

            if (colorEffect.getColorType() != ColorType.Default) {
                onChangeEffect?.Invoke(colorEffect.getColorType());
            }

            onChangeEffect?.Invoke(currentColorType);
            currentColorEffect.RemoveEffect(gameObject);
            currentColorEffect = null;
            currentColorType = ColorType.Default;
        }
    }

    void Start()
    {
        currentColorType = ColorType.Default;
        initialScale = transform.localScale;
    }

    void Update()
    {
        StrechEffect();
        RevertStrechEffect();
    }

    private void StrechEffect() {
        if (currentColorType == ColorType.Strech) { 
            IStrechEffect effect = currentColorEffect as IStrechEffect;
            effect.ApplyEffect();
        }
    }

    private void RevertStrechEffect() {
        if (lastStrechEffect != null) {
            if (!lastStrechEffect.getRevertingEffect())
            {
                lastStrechEffect.RemoveEffect(gameObject);
            }
            else {
                lastStrechEffect = null;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (currentColorEffect != null) {
            if (currentColorType == ColorType.Elastic && collision.gameObject.CompareTag("Player"))
            {
                IElasticEffect effect = currentColorEffect as IElasticEffect;
                effect.ApplyEffect(collision.gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (currentColorEffect != null) {
            if (currentColorType == ColorType.Water && collision.gameObject.CompareTag("Player")) { 
                IWaterEffect effect = currentColorEffect as IWaterEffect;
                effect.ApplyEffect();
            }
        }
    }

    public Vector3 getInitialScale() { 
        return initialScale;
    }
}
