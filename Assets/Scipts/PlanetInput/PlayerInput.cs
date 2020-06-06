using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : IPlanetInput
{
    private Transform   parent;
    private Planet      parentPlanet;
    private Vector3     lookEulerAngle;
    private Action      onShoot;
    public Vector3      LookEulerAngle { get { return lookEulerAngle; } }
    public Action       OnShoot { set { onShoot = value; } }

    private float moveSpeedStep = 10.0f;
    private float moveMinSpeed = 10.0f;
    private float moveMaxSpeed = 200.0f;

    public PlayerInput(Transform parent)
    {
        this.parent     = parent;
        parentPlanet    = parent.GetComponent<Planet>();
        lookEulerAngle  = Vector3.zero;
    }

    public void UpdateInput()
    {
        UpdateAngleInput();
        UpdateShootInput();
        UpdateMoveSpeedInput();
    }

    private void UpdateAngleInput()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;
        Vector3 dirToMouse = Vector3.Normalize(mouseWorldPos - parent.position);
        lookEulerAngle.z = Vector3.SignedAngle(dirToMouse, Vector3.up, Vector3.back);
    }

    private void UpdateShootInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            onShoot();
        }
    }

    private void UpdateMoveSpeedInput()
    {
        float currentSpeed = parentPlanet.RotateSpeed;
        if (Input.GetKeyDown(KeyCode.Q))
        {
            currentSpeed -= moveSpeedStep;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            currentSpeed += moveSpeedStep;
        }
        parentPlanet.RotateSpeed = Mathf.Clamp(currentSpeed, moveMinSpeed, moveMaxSpeed);
    }
}
