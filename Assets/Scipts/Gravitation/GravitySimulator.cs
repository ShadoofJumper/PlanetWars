using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySimulator : MonoBehaviour
{

    #region Singlton

    public static GravitySimulator instance;

    private void SetupSinglton()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("Try create another instance of game manager!");
        }

    }
    #endregion

    [SerializeField] private float globalGravity;

    private float physicalTimeStep = 0.01f;
    private List<GravityBody> bodies = new List<GravityBody>();
    public float GlobalGravity => globalGravity;

    public void AddBodieToSystem(GravityBody body)
    {
        bodies.Add(body);
    }

    private void Awake()
    {
        SetupSinglton();
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < bodies.Count; i++)
        {
            if (bodies[i].gameObject.activeSelf && bodies[i].IsAffectByGravity)
            {
                bodies[i].UpdateBodyForce(bodies);
            }
        }
    }


}
