using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInputs : MonoBehaviour
{
    private static PlayerInputs instance;
    public static PlayerInputs Instance
    {
        get { 
            if (instance == null)
                instance = FindAnyObjectByType<PlayerInputs>();
            return instance;
        }
    }

    [Header("CONTROLES")]
    [SerializeField] private KeyCode pauseGame;
    [SerializeField] private KeyCode shootKey;
    [SerializeField] private KeyCode jumpKey;

    [SerializeField] private KeyCode swapColor;

    //Tecla para disparar
    public event Action onShoot;

    //Movimiento
    public event Action onJump;

    //Cambiar Colores
    public event Action onSwapColor;

    //Pausar Juego
    public event Action onPauseGame;


    private void Update()
    {
        if (Input.GetKeyDown(shootKey) || Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Mouse0))
            onShoot?.Invoke();

        if(Input.GetKeyDown(jumpKey))
            onJump?.Invoke();

        //COLORS
        if(Input.GetKeyDown(swapColor))
            onSwapColor?.Invoke();

        if(Input.GetKeyDown(pauseGame))
            onPauseGame?.Invoke();
    }
}
