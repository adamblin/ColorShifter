using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrechEffect : IStrechEffect
{
    private Color effectColor;
    private ColorType colorType;
    private float stretchAmount;
    
    private Color previousColor;
    private GameObject obstacle;
    private Vector3 initialScale;

    private bool doneRevertingEffect = false;
    private bool revertedColor = false;
    
    public StrechEffect(Color color, ColorType colorType, float multiplier)
    {
        effectColor = color;
        this.colorType = colorType;
        stretchAmount = multiplier;
    }



    public void InitializeEffect(GameObject target)
    {
        obstacle = target;
        previousColor = target.GetComponent<SpriteRenderer>().color;
        target.GetComponent<SpriteRenderer>().color = effectColor;
        initialScale = target.transform.localScale;

        Debug.Log("initialScale"+initialScale);
        
        Debug.Log("APLICANDO EFECTO");
    }

    public void ApplyEffect()
    {
        Debug.Log("Streching");
        obstacle.transform.position = new Vector2(obstacle.transform.position.x, obstacle.transform.position.y + (stretchAmount / 2));
        obstacle.transform.localScale = new Vector2(obstacle.transform.localScale.x, obstacle.transform.localScale.y + stretchAmount);
    }

    public ColorType getColorType()
    {
        return colorType;
    }

    public void RemoveEffect(GameObject target)
    {
        //Restauramos el color y la altura originales

        if (!revertedColor) {
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
