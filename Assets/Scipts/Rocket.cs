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

    private float delayDestroy = 7.0f;
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
    public void StartRocketMove()
    {
        StartCoroutine(DestroyRocketAfterTime(delayDestroy));
    }

    IEnumerator DestroyRocketAfterTime(float delay)
    {
        yield return new WaitForSeconds(delay);
        // after time, if bullet actiove then destroy
        if (gameObject.activeSelf)
        {
            //destroy bullet
            OnRocketDestroy(this, gameObject);
        }
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
