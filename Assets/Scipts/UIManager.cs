using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region UIManager

    public static UIManager instance;

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

    [SerializeField] private GameObject uiInfoBarOrigin;
    [SerializeField] private Transform planetsUIFolder;

    public GameObject UIInfoBarOrigin   => uiInfoBarOrigin;
    public Transform PlanetsUIFolder    => planetsUIFolder;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
