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
    private BoxCollider2D boxCollider;

    
    public StrechEffect(Color color, ColorType colorType, float multiplier)
    {
        effectColor = color;
        this.colorType = colorType;
        this.stretchAmount = multiplier;
    }



    public void InitializeEffect(GameObject target)
    {
        this.obstacle = target;
        previousColor = target.GetComponent<SpriteRenderer>().color;
        target.GetComponent<SpriteRenderer>().color = effectColor;
        initialScale = target.transform.localScale;
        boxCollider = target.GetComponent<BoxCollider2D>();

        //Ajustamos el collider para tener en cuenta el estiramiento
        if (boxCollider != null)
        {
            Vector2 newColliderSize = new Vector2(boxCollider.size.x, boxCollider.size.y * stretchAmount);
            Vector2 colliderOffset = new Vector2(boxCollider.offset.x, boxCollider.offset.y * stretchAmount);
            boxCollider.size = newColliderSize;
            boxCollider.offset = colliderOffset;
        }
        Debug.Log("APLICANDO EFECTO");
    }

    public void ApplyEffect()
    {
        //Aplicamos el efecto para aumentar la altura
        obstacle.transform.localScale = new Vector3(initialScale.x, initialScale.y * stretchAmount, initialScale.z);
    }

    public ColorType getColorType()
    {
        return colorType;
    }

    public void RemoveEffect(GameObject target)
    {
        //Restauramos el color y la altura originales
        target.GetComponent<SpriteRenderer>().color = previousColor;
        target.transform.localScale = initialScale;

        // Restauramos tambien el collider
        if (boxCollider != null)
        {
            boxCollider.size = new Vector2(boxCollider.size.x, boxCollider.size.y / stretchAmount);
            boxCollider.offset = new Vector2(boxCollider.offset.x, boxCollider.offset.y / stretchAmount);
        }
    }
}
