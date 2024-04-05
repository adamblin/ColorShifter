using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInputs : MonoBehaviour
{
    public static event Action onShoot;
    public static event Action onWPressed;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1")) {
            Debug.Log("ShootKey Pressed");
            onShoot?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            onWPressed?.Invoke();  
        }
    }
}
