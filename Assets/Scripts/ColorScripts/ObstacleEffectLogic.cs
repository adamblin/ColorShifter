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

    //LOGICA STRECH
    private IStrechEffect lastStrechEffect;

    public void ApplyEffect(IColorEffect colorEffect) {
        if (currentColorEffect != null)
        {
            if (currentColorType == ColorType.Strech) { 
                IStrechEffect effect = currentColorEffect as IStrechEffect; 
                lastStrechEffect = effect;
            }

            currentColorEffect.RemoveEffect(gameObject);
            currentColorEffect = null;
            currentColorType = ColorType.Default;
        }
        else 
        {
            currentColorEffect = colorEffect;
            currentColorEffect.InitializeEffect(gameObject);
            currentColorType = currentColorEffect.getColorType();
        }
    }
    
    void Start()
    {
        currentColorType = ColorType.Default;
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
        Debug.Log("Collided");
        if (currentColorEffect != null)
            if (currentColorType == ColorType.Elastic && collision.gameObject.CompareTag("Player")) {

                IElasticEffect effect = currentColorEffect as IElasticEffect;
                effect.ApplyEffect(collision.gameObject);
        }
    }
}
