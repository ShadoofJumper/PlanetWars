using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIInput : IPlanetInput
{
    private Vector3 lookEulerAngle;
    public  Vector3 LookEulerAngle => lookEulerAngle;
    private Action onShoot;
    public  Action OnShoot { set { onShoot = value; } }

    public AIInput()
    {

    }

    public void UpdateInput()
    {
    }

}
