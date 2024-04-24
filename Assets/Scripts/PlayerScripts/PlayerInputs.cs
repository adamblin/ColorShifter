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

    [Header("BLOQUEAR EFECTOS ESCENAS")]
    [SerializeField] private bool cantUseStrech = false;
    [SerializeField] private bool cantUseElastic = false;
    [SerializeField] private bool cantUseWater = false;

    [Header("CONTROLES")]
    [SerializeField] private KeyCode shootKey;
    [SerializeField] private KeyCode jumpKey;

    [SerializeField] private KeyCode ElasticKey;
    [SerializeField] private KeyCode WaterKey;
    [SerializeField] private KeyCode StrechKey;

    //Tecla para disparar
    public event Action onShoot;

    //Movimiento
    public event Action onJump;

    //Cambiar Colores
    public event Action<ColorType> onChangeColor;


    private void Update()
    {
        if (Input.GetKeyDown(shootKey) || Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Mouse0))
            onShoot?.Invoke();

        if(Input.GetKeyDown(jumpKey))
            onJump?.Invoke();

        //COLORS
        if (Input.GetKeyDown(ElasticKey) && !cantUseElastic) 
            onChangeColor?.Invoke(ColorType.Elastic);

        if (Input.GetKeyDown(WaterKey) && !cantUseWater)
            onChangeColor?.Invoke(ColorType.Water);

        if (Input.GetKeyDown(StrechKey) && !cantUseStrech)
            onChangeColor?.Invoke(ColorType.Strech);
    }
}
