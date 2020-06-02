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
    //open for test
    public List<GravityBody> bodies = new List<GravityBody>();
    public float GlobalGravity => globalGravity;

    private void Awake()
    {
        SetupSinglton();
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < bodies.Count; i++)
        {
            if (bodies[i].IsAffectByGravity)
            {
                bodies[i].UpdateBodyForce(bodies);
            }
        }
    }


}
