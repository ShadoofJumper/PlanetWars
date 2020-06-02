using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GravityBody : MonoBehaviour
{
    [SerializeField] private float mass;
    [SerializeField] private float radius;
    [SerializeField] private Vector3 startVelocity;
    [SerializeField] private bool isAffectByGravity;
    public bool IsAffectByGravity => isAffectByGravity;

    private Vector3 currentVelocity;
    private Rigidbody rb;


    private void Awake()
    {
        currentVelocity = startVelocity;
        rb = GetComponent<Rigidbody>();
        rb.mass = mass;
    }


    public void UpdateBodyForce(List<GravityBody> otherBodies)
    {
        //apply force of all bodies to this body
        foreach (var otherBody in otherBodies)
        {
            if (otherBody != this)
            {
                Vector3 forceDir        = otherBody.transform.position - transform.position;
                Vector3 normForceDir    = forceDir.normalized;
                float sqrDistance       = forceDir.sqrMagnitude;

                Vector3 force = normForceDir * GravitySimulator.instance.GlobalGravity * (mass * otherBody.mass) / sqrDistance;
                Vector3 modifyForce = force + currentVelocity;
                rb.AddForce(modifyForce);
            }
        }
    }



}
