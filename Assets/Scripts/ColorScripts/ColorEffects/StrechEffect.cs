using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StrechEffect : IStrechEffect
{
    private Color effectColor;
    private ColorType colorType;
    private float stretchAmount;
    private LayerMask layerMask;
    
    private Color previousColor;
    private GameObject obstacle;
    private Vector3 initialScale;

    private bool doneRevertingEffect = false;
    private bool revertedColor = false;
    
    public StrechEffect(Color color, ColorType colorType, float multiplier, LayerMask layerMask)
    {
        effectColor = color;
        this.colorType = colorType;
        stretchAmount = multiplier;
        this.layerMask = layerMask;
    }

    public void InitializeEffect(GameObject target)
    {
        obstacle = target;
        previousColor = target.GetComponent<SpriteRenderer>().color;
        target.GetComponent<SpriteRenderer>().color = effectColor;
        initialScale = target.GetComponent<ObstacleEffectLogic>().getInitialScale();
    }

    public void ApplyEffect()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(obstacle.transform.position, Vector2.up, obstacle.transform.localScale.y / 2, layerMask);

        if (hits.Length == 1) {
            StrechObject(false);
        }
    }

    public ColorType getColorType()
    {
        return colorType;
    }

    private void StrechObject(bool inverted) {
        int obstacleRotation = (int)obstacle.transform.rotation.z;

        if (obstacleRotation == 90) { 
            
        }


        obstacle.transform.position = new Vector2(obstacle.transform.position.x, obstacle.transform.position.y + (stretchAmount / 2));
        obstacle.transform.localScale = new Vector2(obstacle.transform.localScale.x, obstacle.transform.localScale.y + stretchAmount);
    }

    public void RemoveEffect(GameObject target)
    {
        //Restauramos el color y la altura originales

        if (!revertedColor)
        {
            target.GetComponent<SpriteRenderer>().color = previousColor;
            revertedColor = true;
        }


        if (target.transform.localScale.y > initialScale.y)
        {
            obstacle.transform.position = new Vector2(obstacle.transform.position.x, obstacle.transform.position.y - (stretchAmount / 2));
            obstacle.transform.localScale = new Vector2(obstacle.transform.localScale.x, obstacle.transform.localScale.y - stretchAmount);
        }
        else {
            doneRevertingEffect = true;
        }
    }


    public bool getRevertingEffect() {
        return doneRevertingEffect;    
    }

}
