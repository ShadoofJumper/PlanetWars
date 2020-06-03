using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : IPlanetInput
{
    private Transform   parent;
    private Vector3     lookEulerAngle;
    private Action      onShoot;
    public Vector3      LookEulerAngle { get { return lookEulerAngle; } }
    public Action       OnShoot { set { onShoot = value; } }

    public PlayerInput(Transform parent)
    {
        this.parent     = parent;
        lookEulerAngle  = Vector3.zero;
    }

    public void UpdateInput()
    {
        // angle
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;
        Vector3 dirToMouse = Vector3.Normalize(mouseWorldPos - parent.position);
        Debug.DrawLine(parent.position, parent.position + dirToMouse * 3, Color.red);
        //Debug.DrawLine(parent.position, parent.position + Vector3.up * 3, Color.blue);
        lookEulerAngle.z = Vector3.SignedAngle(dirToMouse, Vector3.up, Vector3.back);
        //shoot
        if (Input.GetMouseButtonDown(0))
        {
            onShoot();
        }
    }
}
