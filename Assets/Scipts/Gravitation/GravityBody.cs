﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GravityBody : MonoBehaviour
{
    [SerializeField] private float mass;
    [SerializeField] private float radiusGravityApply = 1.0f;
    [SerializeField] private Vector3 startVelocity;
    [SerializeField] private bool isAffectByGravity;
    [SerializeField] private bool isSun = false;

    public float RadiusGravityApply { get { return radiusGravityApply; }    set { radiusGravityApply = value; } }
    public bool IsAffectByGravity   => isAffectByGravity;
    public bool IsSun               { get { return isSun; } set { isSun = value; } }

    private Rigidbody rb;
    private Rocket rocket;


    private void Awake()
    {
        // add body to simulate system
        GravitySimulator.instance.AddBodyToSystem(this);
        rb      = GetComponent<Rigidbody>();
        rocket  = GetComponent<Rocket>();
        rb.mass = mass;
    }

    public void UpdateBodyMass(float mass)
    {
        this.mass   = mass;
        rb.mass     = mass;
    }



    public void UpdateBodyForce(List<GravityBody> otherBodies)
    {
        //apply force of all bodies to this body
        foreach (var otherBody in otherBodies)
        {
            if (otherBody != this && !IsBodyParent(otherBody))//here need condition
            {
                Vector3 forceDir        = otherBody.transform.position - transform.position;
                Vector3 normForceDir    = forceDir.normalized;
                float sqrDistance       = forceDir.sqrMagnitude;

                Vector3 force = normForceDir * GravitySimulator.instance.GlobalGravity * (mass * otherBody.mass) / sqrDistance;

                //force apply condition 
                //if (otherBody.IsSun)
                //{
                    force = force / sqrDistance;
                    rb.AddForce(force);
                //}
                //else
                //{
                    //if (forceDir.magnitude <= otherBody.radiusGravityApply)
                    //{
                        //rb.AddForce(force);
                        //Debug.Log("Affect by: " + otherBody.name);
                    //}
                //}
            }
        }
    }


    private bool IsBodyParent(GravityBody otherBody)
    {
        //check if this body rocket and other body rocket parent
        Planet otherBodyPlanet = otherBody.GetComponent<Planet>();
        if (rocket != null 
            && otherBodyPlanet != null 
            && rocket.ParentPlanet == otherBodyPlanet)
        {
            return true;
        }
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radiusGravityApply);
    }



}
