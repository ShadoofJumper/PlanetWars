using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    #region Singlton

    public static ScoreManager instance;

    private void Awake()
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
    private int planetsDestroy;
    private int startPlanetsInGame;

    private void Start()
    {
        startPlanetsInGame = SceneController.instance.PlanetInGameCount;
    }

    public int GetAlivePlanets()
    {
        return startPlanetsInGame - planetsDestroy;
    }

    public void IncreaseScore()
    {
        planetsDestroy++;
    }


}
