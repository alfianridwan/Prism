using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    public enum GameState
    {
        PLAY,
        PAUSED,
        LEVEL_COMPLETE
    }

    public GameState gameState;
    public Player playerOne;
    public Player playerTwo;
    public UIController UI;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        switch (gameState)
        {
            case GameState.PLAY:
                playerOne.isMovable = true;
                playerTwo.isMovable = true;
                break;

            case GameState.PAUSED:
                playerOne.isMovable = false;
                playerTwo.isMovable = false;
                break;

            case GameState.LEVEL_COMPLETE:
                playerOne.isMovable = false;
                playerTwo.isMovable = false;
                break;
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            SwapPlayer();
        }
    }

    public void ChangeGameState(GameState newState) => gameState = newState;

    public GameState GetGameState() => gameState;

    public void SwapPlayer()
    {
        playerOne.ChangePlayerIndex(TWO);
        playerTwo.ChangePlayerIndex(ONE);
    }
}
