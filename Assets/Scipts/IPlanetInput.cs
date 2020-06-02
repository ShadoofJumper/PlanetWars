using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlanetInput
{
    Vector3 LookEulerAngle { get; }
    Action OnShoot { set; }
    void UpdateInput();
}
