using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetUI : MonoBehaviour
{
    private Planet parentPlanet;
    private GameObject  uiInfoBarOrigin;
    private Transform   planetsUIFolder;
    private GameObject  planetUI;
    private InfoBar     infoBar;

    public InfoBar InfoBar => infoBar;


    private void Start()
    {
        parentPlanet = GetComponent<Planet>();
        uiInfoBarOrigin = UIManager.instance.UIInfoBarOrigin;
        planetsUIFolder = UIManager.instance.PlanetsUIFolder;
        if (!parentPlanet.IsSun)
            CreatePlanetUI();
    }


    void Update()
    {
        if (!parentPlanet.IsSun)
            UpdatePositionUI();
    }

    private void CreatePlanetUI()
    {
        planetUI    = Instantiate(uiInfoBarOrigin, planetsUIFolder);
        infoBar     = planetUI.GetComponent<InfoBar>();
        infoBar.UpdateHP(SceneController.instance.PlanetsHealth);
    }

    private void UpdatePositionUI()
    {
        planetUI.transform.position = Camera.main.WorldToScreenPoint(transform.position);
    }

    private void OnDestroy()
    {
        Destroy(planetUI);
    }


}
