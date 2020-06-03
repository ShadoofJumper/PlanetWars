﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    private IPlanetInput planetInput;
    private int     health;
    private Rocket  originRocket;
    private float   fireRate;
    private float   timeToFire;
    private Transform   shootSpot;
    private Transform   shootSpotRotateParent;
    private Planet      parentPlanet;

    private Queue<GameObject> rocketPool = new Queue<GameObject>();
    private int rocketPoolCounter;
    private GameObject rocketsParent;

    public void InitializePlanetCombat(IPlanetInput planetInput, int health, Rocket originRocket, Transform shootSpot)
    {
        this.planetInput    = planetInput;
        this.health         = health;
        this.originRocket   = originRocket;
        this.shootSpot      = shootSpot;
        fireRate            = originRocket.Cooldown;
        SetUpCombat();
    }

    private void SetUpCombat()
    {
        // call shoot from input
        planetInput.OnShoot = Shoot;
        // get components
        shootSpotRotateParent = shootSpot.GetComponentInParent<Transform>();
        parentPlanet = gameObject.GetComponent<Planet>();
        EnableShootSpot();
        //create folder for rockets
        rocketsParent = new GameObject();
        rocketsParent.name = "Rockets from: " + gameObject.name;
    }

    private void EnableShootSpot()
    {
        float distFromPlanet = parentPlanet.Size + 0.75f;
        shootSpot.gameObject.SetActive(true);
        shootSpot.localPosition = Vector3.up * distFromPlanet;
    }

    private void Update()
    {
        RotateShooter(planetInput.LookEulerAngle);
    }

    private void RotateShooter(Vector3 eulerAngle)
    {
        shootSpotRotateParent.localEulerAngles = eulerAngle;
    }

    private void Shoot()
    {
        // check if time to shoot
        if (Time.time >= timeToFire)
        {
            timeToFire = Time.time + 1 /  fireRate;
            CreateRockets();
        }
    }

    private void CreateRockets()
    {
        Debug.Log("CreateRockets");
        Debug.Log("CreateRockets: start angle:"+ planetInput.LookEulerAngle);
        Rocket newRocket = rocketPool.Count != 0 ? GetRocket() : CreateRocket();
        newRocket.StartRocketMove();
        newRocket.transform.rotation = Quaternion.Euler(planetInput.LookEulerAngle);
    }

    private Rocket CreateRocket()
    {
        GameObject newRocketObject  = Instantiate(originRocket.gameObject, shootSpot.position, Quaternion.identity);
        Rocket newRocket            = newRocketObject.GetComponent<Rocket>();
        newRocket.OnRocketDestroy   = OnRocketDestroy;
        newRocketObject.transform.SetParent(rocketsParent.transform);
        newRocketObject.name = "Rocket_" + rocketPoolCounter;
        rocketPoolCounter++;
        return newRocket;
    }

    private Rocket GetRocket()
    {
        GameObject rocketGameObject         = rocketPool.Dequeue();
        Rocket rocket                       = rocketGameObject.GetComponent<Rocket>();
        rocketGameObject.transform.position = shootSpot.position;
        rocketGameObject.SetActive(true);
        return rocket;
    }

    //call when bullet destroy
    private void OnRocketDestroy(Rocket rocket, GameObject rocketObject)
    {
        // remove from queue
        // get bullet
        rocketObject.SetActive(false);
        rocketObject.transform.position = Vector3.zero;
        // add to pool when complete
        rocketPool.Enqueue(rocketObject);
    }
}
