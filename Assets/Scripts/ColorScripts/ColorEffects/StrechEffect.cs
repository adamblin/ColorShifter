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
        RaycastHit2D[] hits = Physics2D.RaycastAll(obstacle.transform.position, obstacle.transform.up, obstacle.transform.localScale.y / 2, layerMask);
        Debug.Log("APLLY");
        if (hits.Length == 1) {
            StrechObject(false);
        }
    }

    private void StrechObject(bool inverted) {
        int obstacleRotation = (int)obstacle.transform.eulerAngles.z;

        switch (obstacleRotation) {

            case 90: 
                if(!inverted)
                    obstacle.transform.position = new Vector2(obstacle.transform.position.x - (stretchAmount / 2), obstacle.transform.position.y);
                else
                    obstacle.transform.position = new Vector2(obstacle.transform.position.x + (stretchAmount / 2), obstacle.transform.position.y);
                break;


            case 180:
                if (!inverted)
                    obstacle.transform.position = new Vector2(obstacle.transform.localPosition.x, obstacle.transform.localPosition.y - (stretchAmount / 2));
                else
                    obstacle.transform.position = new Vector2(obstacle.transform.localPosition.x, obstacle.transform.localPosition.y + (stretchAmount / 2));
                break;


            case 270:
                if (!inverted)
                    obstacle.transform.position = new Vector2(obstacle.transform.position.x + (stretchAmount / 2), obstacle.transform.position.y);
                else
                    obstacle.transform.position = new Vector2(obstacle.transform.position.x - (stretchAmount / 2), obstacle.transform.position.y);
                break;


            default:
                if (!inverted)
                    obstacle.transform.position = new Vector2(obstacle.transform.localPosition.x, obstacle.transform.localPosition.y + (stretchAmount / 2));
                else
                    obstacle.transform.position = new Vector2(obstacle.transform.position.x, obstacle.transform.position.y - (stretchAmount / 2));
                    break;

        }

        if(!inverted)
            obstacle.transform.localScale = new Vector2(obstacle.transform.localScale.x, obstacle.transform.localScale.y + stretchAmount);
        else
            obstacle.transform.localScale = new Vector2(obstacle.transform.localScale.x, obstacle.transform.localScale.y - stretchAmount);
    }

    public void RemoveEffect(GameObject target)
    {
        if (!revertedColor)
        {
            target.GetComponent<SpriteRenderer>().color = previousColor;
            revertedColor = true;
        }


        if (target.transform.localScale.y > initialScale.y)
        {
            StrechObject(true);
        }
        else {
            doneRevertingEffect = true;
        }
    }


    public bool getRevertingEffect() {
        return doneRevertingEffect;    
    }

    public ColorType getColorType()
    {
        return colorType;
    }

}
