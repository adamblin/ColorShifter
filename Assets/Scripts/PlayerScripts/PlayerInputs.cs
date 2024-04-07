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


    //Tecla para disparar
    public static event Action onShoot;

    //Disparar hacia arriba
    public static event Action onShootUp;

    //Disparar hacia abajo
    public static event Action onShootDown;

    //Disparar a los lados
    public static event Action onShootStraight;

    //Movimiento
    public static event Action onMoveLeft;
    public static event Action onStopMoveLeft;
    public static event Action onMoveRight;
    public static event Action onStopMoveRight;

    public static event Action onJump;



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
        if (Input.GetKeyDown(leftKey))
        {
            onMoveLeft?.Invoke();
        }
        else if (Input.GetKeyUp(leftKey))
        {
            onStopMoveLeft?.Invoke();
        }

        if (Input.GetKeyDown(rightKey))
        {
            onMoveRight?.Invoke();
        }
        else if (Input.GetKeyUp(rightKey))
        {
            onStopMoveRight?.Invoke();
        }

        if (Input.GetKeyDown(jumpKey))
        {
            onJump?.Invoke();
        }
    }
}
