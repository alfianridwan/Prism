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
    public GameObject mainMenu;

    public void Update()
    {
        if (GameController.Instance.gameState == GameState.MAINMENU)
        {
            // if press any key, use LeanTween to fade out Main Menu then change game state to PLAY
            if (Input.anyKeyDown)
            {
                LeanTween.alphaCanvas(mainMenu.GetComponent<CanvasGroup>(), 0.0f, 1.0f).setOnComplete(() =>
                {
                    GameController.Instance.BeginGame();
                });
            }
        }

        // ESCAPE key to show/hide pause menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameController.Instance.gameState == GameState.PLAY)
            {
                GameController.Instance.gameState = GameState.PAUSED;
                ShowPauseMenu();
            }
            else if (GameController.Instance.gameState == GameState.PAUSED)
            {
                GameController.Instance.gameState = GameState.PLAY;
                HidePauseMenu();
            }
        }
    }

    public void UpdateTimer(float timeRemaining, float timeLimit)
    {
        timerFill.fillAmount = timeRemaining / timeLimit;
    }

    public void BackToMainMenu()
    {
        // reload scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
