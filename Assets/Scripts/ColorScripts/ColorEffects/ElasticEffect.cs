using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ElasticEffect : IElasticEffect
{
    private Color effectColor;
    private ColorType colorType;
    private float minImpulse;
    private Color previousColor;

    private GameObject obstacle;

    public ElasticEffect(Color color, ColorType colorType, float minImpulse)
    {
        effectColor = color;
        this.colorType = colorType;
        this.minImpulse = minImpulse;
    }


    public void InitializeEffect(GameObject target)
    {
        obstacle = target;
        previousColor = target.GetComponent<SpriteRenderer>().color;
        target.GetComponent<SpriteRenderer>().color = effectColor;
    }

    public void ApplyEffect(GameObject player)
    {
        //MEDIANTE ADDFORCE


        //MEDIANTE MATERIAL BOUNCE 
        //float newBounciness = obstacle.transform.position.y - player.GetComponent<CharacterMovement>().getLastJumpPosition().y;

        //PhysicsMaterial2D newMaterial = new PhysicsMaterial2D();
        //newMaterial.bounciness = math.abs(newBounciness);

        //obstacle.GetComponent<Rigidbody2D>().sharedMaterial = newMaterial;
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
    

