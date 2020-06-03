using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetUI : MonoBehaviour
{
    private Planet parentPlanet;
    private GameObject  uiInfoBarOrigin;
    private Transform   planetsUIFolder;
    private GameObject  planeyUI;
    private InfoBar     infoBar;

    public InfoBar InfoBar => infoBar;

    // Start is called before the first frame update
    void Start()
    {
        parentPlanet    = GetComponent<Planet>();
        uiInfoBarOrigin = UIManager.instance.UIInfoBarOrigin;
        planetsUIFolder = UIManager.instance.PlanetsUIFolder;
        if(!parentPlanet.IsSun)
            CreatePlanetUI();
    }

    void Update()
    {
        if (!parentPlanet.IsSun)
            UpdatePositionUI();
    }

    private void CreatePlanetUI()
    {
        planeyUI    = Instantiate(uiInfoBarOrigin, planetsUIFolder);
        infoBar     = planeyUI.GetComponent<InfoBar>();
    }

    private void UpdatePositionUI()
    {
        planeyUI.transform.position = Camera.main.WorldToScreenPoint(transform.position);
    }

    private void OnDestroy()
    {
        Destroy(planeyUI);
    }


}
