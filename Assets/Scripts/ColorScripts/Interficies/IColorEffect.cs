using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IColorEffect {

    void InitializeEffect(GameObject target);

    void ApplyEffect();

    ColorType getColorType();
}
