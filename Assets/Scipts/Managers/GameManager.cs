using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singlton

    public static GameManager instance;

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

    [SerializeField] private bool gameInProgress;
    [SerializeField] private int startDelay;

    public bool GameInProgress => gameInProgress;

    void Start()
    {
        StartCoroutine(StartGameWithDelay(startDelay));
    }

    void Update()
    {
    }

    IEnumerator StartGameWithDelay(int delay)
    {
        for (int i = 0; i < delay; i++)
        {
            string time = (startDelay - i).ToString();
            UIManager.instance.SetWarningText(time);
            yield return new WaitForSeconds(1.0f);
        }
        UIManager.instance.SetWarningText("GO!");
        yield return new WaitForSeconds(1.0f);
        UIManager.instance.SetWarningText("");
        StartGame();
    }

    private void StartGame()
    {
        gameInProgress = true;
    }

    private void CompleteGame()
    {
        UIManager.instance.SetWarningText("WIN!");
        UIManager.instance.ShowCompletePanel();
        gameInProgress = false;
    }

    public void FailGame()
    {
        UIManager.instance.SetWarningText("FAIL!");
        UIManager.instance.ShowCompletePanel();
        gameInProgress = false;
    }


    public void CheckCompleteGame()
    {
        if (ScoreManager.instance.GetAlivePlanets() == 1)
        {
            CompleteGame();
        }
    }

    public void PauseGameLogic()
    {
        Time.timeScale = 0f;
    }

    public void ResumeGameLogic()
    {
        Time.timeScale = 1f;
    }

}
