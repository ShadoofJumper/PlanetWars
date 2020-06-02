using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] private float startAcceleration;
    [SerializeField] private float weight;
    [SerializeField] private float cooldown;
    [SerializeField] private float damage;
    private Rigidbody rockerRig;
    private float acceleration;

    public float Acceleration   { get { return acceleration;  } set { acceleration = value; } }
    public float Cooldown       { get { return cooldown; } }
    public Action<Rocket, GameObject> OnRocketDestroy;


    private void Awake()
    {
        rockerRig       = GetComponent<Rigidbody>();
        acceleration    = startAcceleration;
        rockerRig.mass  = weight;
    }


    private void FixedUpdate()
    {
        MoveRocket();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //destroy bullet
        OnRocketDestroy(this, gameObject);
    }

    private void MoveRocket()
    {
        rockerRig.MovePosition(rockerRig.position + transform.up * acceleration * Time.deltaTime);
    }
}
