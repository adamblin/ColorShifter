using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IColorEffect {

    void InitializeEffect(GameObject target);

    void ApplyEffect(GameObject target);

    IColorEffect RemoveEffect(GameObject target);

    ColorType getColorType();
}
