using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInputs : MonoBehaviour
{
    [SerializeField] private KeyCode shootKey;
    [SerializeField] private KeyCode upKey;
    [SerializeField] private KeyCode downKey;
    [SerializeField] private KeyCode leftKey;
    [SerializeField] private KeyCode rightKey;
    [SerializeField] private KeyCode jumpKey;

    [SerializeField] private KeyCode ElasticKey;
    [SerializeField] private KeyCode WaterKey;
    [SerializeField] private KeyCode StrechKey;

    //Tecla para disparar
    public static event Action onShoot;

    //Disparar hacia arriba
    public static event Action onShootUp;

    //Disparar hacia abajo
    public static event Action onShootDown;

    //Disparar a los lados
    public static event Action onShootStraight;

    //Movimiento
    public static event Action onJump;

    //Cambiar Colores
    public static event Action<ColorType> onChangeColor;


    private void Update()
    {
        if (Input.GetKeyDown(shootKey) || Input.GetButtonDown("Fire1"))
            onShoot?.Invoke();

        if (Input.GetKeyDown(upKey))
            onShootUp?.Invoke();

        if (Input.GetKeyUp(upKey))
            onShootUp?.Invoke();

        if (Input.GetKeyDown(downKey))
            onShootDown?.Invoke();

        if (Input.GetKeyUp(downKey))
            onShootDown?.Invoke();

        if (Input.GetKeyDown(rightKey) || Input.GetKeyDown(leftKey))
            onShootStraight?.Invoke();

        if (Input.GetKeyUp(rightKey) || Input.GetKeyUp(leftKey))
            onShootStraight?.Invoke();

        if(Input.GetKeyDown(jumpKey))
            onJump?.Invoke();

        //COLORS
        if (Input.GetKeyDown(ElasticKey)) 
            onChangeColor?.Invoke(ColorType.Elastic);

        if (Input.GetKeyDown(WaterKey))
            onChangeColor?.Invoke(ColorType.Water);

        if (Input.GetKeyDown(StrechKey))
            onChangeColor?.Invoke(ColorType.Strech);
    }
}
