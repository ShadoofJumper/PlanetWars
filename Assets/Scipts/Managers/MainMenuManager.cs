using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    private string sceneOnPlayName = "Game";
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    // ------------- main menu
    public void StartGame()
    {
        SceneManager.LoadScene(sceneOnPlayName);
    }

    public void Exit()
    {
        Application.Quit();
    }


}
