using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SceneController : MonoBehaviour
{

    #region Singlton

    public static SceneController instance;

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

    [SerializeField] private Planet     planetPrefab;
    [SerializeField] private Transform  planetsFolder;
    //first element is sun spritee
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private Rocket[] rockets;
    [SerializeField] private string[] planetNames;
    [Range(1.0f, 2.0f)]
    [SerializeField] private float  distanceBetweenSolarAxis = 1.0f;
    [SerializeField] private float  planetMinSize;
    [SerializeField] private float  planetMaxSize;
    [SerializeField] private float  planetMinSpeed;
    [SerializeField] private float  planetMaxSpeed;
    [SerializeField] private int    playerPlanetNumber;
    [SerializeField] private int    planetsHealth = 100;

    private List<Sprite> spritesPool = new List<Sprite>();
    private List<Planet> planets = new List<Planet>();
    private Planet sun;
    public Planet Sun => sun;

    private void Start()
    {
        FullSpritePool();
        //create sun
        sun = CreatePlanet(1.5f, 0, 5.0f, 0, sprites[0], "Sun");
        CreateAllPlanets();
        AddCombatToPlanets();
    }

    private void AddCombatToPlanets()
    {
        for (int i = 0; i < planets.Count; i++)
        {
            Rocket randomRocketType = rockets[Random.Range(0, rockets.Length)];
            planets[i].AddCombatController(planetsHealth, i == playerPlanetNumber, randomRocketType);
        }
    }


    private void CreateAllPlanets()
    {
        for (int plN = 0; plN < planetNames.Length; plN++)
        {
            Planet newplanet = CreateRandomPlanet(plN);
            planets.Add(newplanet);
        }
    }

    private Planet CreateRandomPlanet(int solarAxis)
    {
        float size              = Random.Range(planetMinSize, planetMaxSize);
        float distanceFromSun   = distanceBetweenSolarAxis * (solarAxis + 1);
        float rotateSpeed       = Random.Range(planetMinSpeed, planetMaxSpeed);
        float startAngle        = Random.Range(10.0f, 250.0f);
        Sprite planetSprite     = GetRandomSprite();
        string planetName       = planetNames[solarAxis];

        return CreatePlanet(size, distanceFromSun, rotateSpeed, startAngle, planetSprite, planetName);
    }


    private Planet CreatePlanet(float size, float distanceFromSun, float rotateSpeed, float startAngle, Sprite planetSprite, string name)
    {
        Planet newPlanet = Instantiate(planetPrefab, planetsFolder);
        newPlanet.InitializePlanet(size, distanceFromSun, rotateSpeed, startAngle, planetSprite);
        newPlanet.name = name;
        return newPlanet;
    }

    private void FullSpritePool()
    {
        spritesPool.AddRange(sprites);
        spritesPool.RemoveAt(0);
    }

    private Sprite GetRandomSprite()
    {
        Sprite rdSprite = spritesPool[Random.Range(0, spritesPool.Count)];
        spritesPool.Remove(rdSprite);
        return rdSprite;
    }

}
