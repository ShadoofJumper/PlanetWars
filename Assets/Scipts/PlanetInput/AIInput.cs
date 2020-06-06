using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIInput : IPlanetInput
{
    private Combat  parentCombat;
    private Vector3 lookEulerAngle;
    private Action  onShoot;
    private float   currentAngle;
    private float   rotateSpeed = 90.0f;

    public Action OnShoot { set { onShoot = value; } }
    public Vector3 LookEulerAngle => lookEulerAngle;

    public AIInput(Combat parentCombat)
    {
        lookEulerAngle = Vector3.zero;
        this.parentCombat = parentCombat;
    }

    public void UpdateInput()
    {
        // angle
        currentAngle += rotateSpeed * Time.deltaTime;
        currentAngle = Mathf.Repeat(currentAngle, 360);
        lookEulerAngle = Vector3.forward * currentAngle;
        // shoot on cd
        if (Time.time >= parentCombat.TimeToFire)
        {
            onShoot();
        }
    }

}
