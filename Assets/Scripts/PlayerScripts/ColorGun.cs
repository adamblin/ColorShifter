using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class ColorGun : MonoBehaviour
{
    private ColorType[] colorTypes;
    private int currentColorIndex = 0;

    private ColorManager colorManager;

    void Start()
    {
        colorTypes = new ColorType[]
        {
            ColorType.Elastic,
            ColorType.Water,
            ColorType.Strech
        };

        colorManager = FindObjectOfType<ColorManager>(); 
    }

    private void ShootTongue() { 
        
    }

}