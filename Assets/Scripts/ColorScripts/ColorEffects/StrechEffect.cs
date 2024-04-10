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
        RaycastHit2D hit = Physics2D.Raycast(obstacle.transform.position, Vector2.up, obstacle.transform.localScale.y / 2, layerMask);
        Debug.Log(hit.collider);
        if (hit.collider == null) {
            obstacle.transform.position = new Vector2(obstacle.transform.position.x, obstacle.transform.position.y + (stretchAmount / 2));
            obstacle.transform.localScale = new Vector2(obstacle.transform.localScale.x, obstacle.transform.localScale.y + stretchAmount);
        }
    }

    public ColorType getColorType()
    {
        return colorType;
    }

    public void RemoveEffect(GameObject target)
    {
        //Restauramos el color y la altura originales

        if (!revertedColor)
        {
            target.GetComponent<SpriteRenderer>().color = previousColor;
            revertedColor = true;
        }

        Debug.Log(target.transform.localScale + " intital Scale: " + initialScale);

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
