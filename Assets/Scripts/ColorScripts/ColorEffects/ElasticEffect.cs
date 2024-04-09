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

        Vector2 direction = -(obstacle.transform.position - player.transform.position).normalized;
        float jumpHeight = Mathf.Abs(player.GetComponent<CharacterMovement>().getLastJumpPosition().y - obstacle.transform.position.y);

        float totalForce = minImpulse + (jumpHeight * hightMultiplier);

        //FALTA OBTENER EL VECTOR EN EL QUE APLCAR LA FUERZA

        Debug.Log(direction);
        //rb.velocity = new Vector2(0, totalForce);
        rb.velocity = direction * totalForce;
        Debug.Log("result: " + direction * totalForce);


    }

    public void RemoveEffect(GameObject target)
    {
        target.GetComponent<SpriteRenderer>().color = previousColor;
        obstacle.GetComponent<Rigidbody2D>().sharedMaterial = null;
    }

    public ColorType getColorType()
    {
        return colorType;
    }

    
}
    

