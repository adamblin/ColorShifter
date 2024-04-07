using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public interface IColorEffect {

    void InitializeEffect(GameObject target);


    void ApplyEffect(GameObject target);

    ColorType getColorType();
}
