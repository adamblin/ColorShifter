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


    //Tecla para disparar
    public static event Action onShoot;

    //Disparar hacia arriba
    public static event Action onShootUp;

    //Disparar hacia abajo
    public static event Action onShootDown;

    //Disparar a los lados
    public static event Action onShootStraight;

    //Movimiento
    public static event Action onMoveUp;
    public static event Action onMoveDown;
    public static event Action onMoveLeft;
    public static event Action onMoveRight;


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
        
        //Movement
        if (Input.GetKey(upKey))
            onMoveUp?.Invoke();

        if (Input.GetKey(downKey))
            onMoveDown?.Invoke();

        if (Input.GetKey(leftKey))
            onMoveLeft?.Invoke();

        if (Input.GetKey(rightKey))
            onMoveRight?.Invoke();
    }
}
