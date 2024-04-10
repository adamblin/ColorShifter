using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStrechEffect : IColorEffect
{
    void ApplyEffect();

    bool getRevertingEffect();
}
