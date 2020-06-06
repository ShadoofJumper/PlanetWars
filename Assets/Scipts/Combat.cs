using System;
using System.Collections;
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
    private PlanetUI    planetUI;

    private static Queue<GameObject> rocketBluePool     = new Queue<GameObject>();
    private static Queue<GameObject> rocketGreenPool    = new Queue<GameObject>();
    private static Queue<GameObject> rocketYellowPool   = new Queue<GameObject>();
    private static int rocketPoolCounter;
    private Queue<GameObject> currentPool;
    

    public float TimeToFire => timeToFire;

    public void InitializePlanetCombat(IPlanetInput planetInput, int health, Rocket originRocket, Transform shootSpot)
    {
        this.planetInput    = planetInput;
        this.health         = health;
        this.originRocket   = originRocket;
        this.shootSpot      = shootSpot;
        fireRate            = originRocket.Cooldown;
        SetUpCombat();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
            Die();
        planetUI.InfoBar.UpdateHP(health);
        if (parentPlanet.IsPlayer)
            UIManager.instance.UpdatePlayerHP(health);
    }

    private void Die()
    {
        GravitySimulator.instance.RemoveBodyFromSystem(gameObject);
        Destroy(gameObject);
        ScoreManager.instance.IncreaseScore();
        GameManager.instance.CheckCompleteGame();
        if (parentPlanet.IsPlayer)
            GameManager.instance.FailGame();
    }

    private void SetUpCombat()
    {
        // call shoot from input
        planetInput.OnShoot = Shoot;
        // get components
        shootSpotRotateParent = shootSpot.GetComponentInParent<Transform>();
        parentPlanet    = gameObject.GetComponent<Planet>();
        planetUI        = gameObject.GetComponent<PlanetUI>();
        EnableShootSpot();
        SetRocketPool(originRocket.RocketParamType);

        if (parentPlanet.IsPlayer)
            UIManager.instance.UpdatePlayerHP(health);
    }

    private void SetRocketPool(RocketType type)
    {
        switch (type)
        {
            case RocketType.Blue:
                currentPool = rocketBluePool;
                break;
            case RocketType.Green:
                currentPool = rocketGreenPool;
                break;
            case RocketType.Yellow:
                currentPool = rocketYellowPool;
                break;
        }
    }

    private void EnableShootSpot()
    {
        float distFromPlanet = 0.75f;
        shootSpot.gameObject.SetActive(true);
        shootSpot.localPosition = Vector3.up * distFromPlanet;
    }

    private void Update()
    {
        if (planetInput!=null)
            RotateShooter(planetInput.LookEulerAngle);
        UpdateUI();
    }

    private void UpdateUI()
    {
        int cdSec = GetCDSeconds();
        planetUI.InfoBar.UpdateCoolDown(cdSec);
        if (parentPlanet.IsPlayer)
            UIManager.instance.UpdatePlayerCD(cdSec);
    }

    private int GetCDSeconds()
    {
        float timeToShoot   = timeToFire - Time.time;
        timeToShoot         = timeToShoot > 0 ? timeToShoot : 0;
        return (int)Mathf.Round(timeToShoot);
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
        Rocket newRocket = currentPool.Count != 0 ? GetRocket() : CreateRocket();
        newRocket.StartRocketMove();
        newRocket.ParentPlanet          = parentPlanet;
        newRocket.transform.rotation    = Quaternion.Euler(planetInput.LookEulerAngle);
    }

    private Rocket CreateRocket()
    {
        GameObject newRocketObject  = Instantiate(originRocket.gameObject, shootSpot.position, Quaternion.identity);
        Rocket newRocket            = newRocketObject.GetComponent<Rocket>();
        newRocket.OnRocketDestroy   = OnRocketDestroy;
        newRocketObject.transform.SetParent(SceneController.instance.RocketsFolder);
        newRocketObject.name = $"{originRocket.name}_{rocketPoolCounter}";
        rocketPoolCounter++;
        return newRocket;
    }

    private Rocket GetRocket()
    {
        GameObject rocketGameObject         = currentPool.Dequeue();
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
        rocketObject.transform.position     = Vector3.zero;
        rocket.RocketRig.velocity           = Vector3.zero;
        rocket.RocketRig.angularVelocity    = Vector3.zero;
        // add to pool when complete
        currentPool.Enqueue(rocketObject);
    }
}
