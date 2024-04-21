using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ElasticEffect : IElasticEffect
{
    private Color effectColor;
    private ColorType colorType;
    private float minImpulse;
    private float hightMultiplier;

    private Color previousColor;
    private GameObject obstacle;

    public ElasticEffect(Color color, ColorType colorType, float minImpulse, float hightMultiplier)
    {
        effectColor = color;
        this.colorType = colorType;
        this.minImpulse = minImpulse;
        this.hightMultiplier = hightMultiplier;
    }


    public void InitializeEffect(GameObject target)
    {
        obstacle = target;
        previousColor = target.GetComponent<SpriteRenderer>().color;
        target.GetComponent<SpriteRenderer>().color = effectColor;
    }

    public void ApplyEffect(GameObject player)
    {
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();

        float jumpHeight = Mathf.Abs(player.GetComponent<CharacterMovement>().getLastJumpPosition().y 
            - (obstacle.transform.position.y + obstacle.transform.localScale.y / 2));

        
        float totalForce = minImpulse + (jumpHeight * hightMultiplier);
        Debug.Log(totalForce);
        
        if (totalForce >= 150)
        {
            totalForce = totalForce / 2f;
        }
        
        rb.velocity = player.transform.up * totalForce;


    }

    public void RemoveEffect(GameObject target)
    {
        target.GetComponent<SpriteRenderer>().color = previousColor;
    }

    public ColorType getColorType()
    {
        return colorType;
    }

    
}
    

