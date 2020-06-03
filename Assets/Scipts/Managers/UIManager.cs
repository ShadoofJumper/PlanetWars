using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject completePanel;
    [SerializeField] private GameObject uiInfoBarOrigin;
    [SerializeField] private Transform planetsUIFolder;
    [SerializeField] private Text warningText;
    [SerializeField] private Text hpText;
    [SerializeField] private Text cdText;

    private string  menuSceneName       = "MainMenu";
    private string  gameSceneName        = "Game";
    private bool    isGameOnPause       = false;
    public GameObject UIInfoBarOrigin   => uiInfoBarOrigin;
    public Transform PlanetsUIFolder    => planetsUIFolder;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("AAAAAAAAAAAAAAAA");
            if (!isGameOnPause)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    // -------- PAUSE PANEL -------
    public void PauseGame()
    {
        GameManager.instance.PauseGameLogic();
        pausePanel.SetActive(true);
        isGameOnPause = true;
    }
    public void ResumeGame()
    {
        GameManager.instance.ResumeGameLogic();
        pausePanel.SetActive(false);
        isGameOnPause = false;
    }
    public void ReturnMenu()
    {
        GameManager.instance.ResumeGameLogic();
        SceneManager.LoadScene(menuSceneName);
    }
    // --------- COMPLETE PANEL ---------

    public void ShowCompletePanel()
    {
        GameManager.instance.PauseGameLogic();
        isGameOnPause = true;
        completePanel.SetActive(true);
    }
    public void HideCompletePanel()
    {
        GameManager.instance.ResumeGameLogic();
        isGameOnPause = false;
        completePanel.SetActive(false);
    }
    public void RestartGame()
    {
        HideCompletePanel();
        SceneManager.LoadScene(gameSceneName);
    }

    // --------- UI TEXT ---------
    public void SetWarningText(string text)
    {
        warningText.text = text;
    }

    public void UpdatePlayerHP(int hp)
    {
        hpText.text = "HP " + hp;
    }

    public void UpdatePlayerCD(int cd)
    {
        cdText.text = cd > 10 ? "CD " + cd : "CD 0" + cd;
    }
}
