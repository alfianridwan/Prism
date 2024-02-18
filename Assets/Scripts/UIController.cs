using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public Image timerFill;
    public GameObject pauseMenu;
    public GameObject gameComplete;

    public void UpdateTimer(float timeRemaining, float timeLimit)
    {
        timerFill.fillAmount = timeRemaining / timeLimit;
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowPauseMenu()
    {
        pauseMenu.SetActive(true);
    }

    public void HidePauseMenu()
    {
        pauseMenu.SetActive(false);
    }

    public void ShowGameComplete()
    {
        gameComplete.SetActive(true);
    }

    public void HideGameComplete()
    {
        gameComplete.SetActive(false);
    }
}
