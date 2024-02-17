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

    [Header("Game Settings")]
    public GameState gameState;
    public float rotateAmount = 45f;
    public float scaleAmount = 0.25f;
    public float minScaleAmount = 0.5f;
    public float maxScaleAmount = 1.5f;
    public float jumpAmount = 1f;
    public float minJumpAmount = 3.0f;
    public float maxJumpAmount = 7.0f;

    [Header("Player Settings")]
    public Player playerOne;
    public Player playerTwo;
    public List<Player> players = new List<Player>();

    [Header("UI")]
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

    private void Start()
    {
        // add playerOne and playerTwo to the List
        players.Add(playerOne);
        players.Add(playerTwo);
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
        playerOne.ChangePlayerIndex(Player.PlayerIndex.TWO);
        playerTwo.ChangePlayerIndex(Player.PlayerIndex.ONE);
    }

    public void RotatePlayers()
    {
        foreach(Player player in players)
        {
            if (player.canTransform)
            {
                player.currentRotation += rotateAmount;

                player.currentRotation %= 360f;

                LeanTween.rotateZ(player.spriteChild.gameObject, player.currentRotation, 0.5f).setEase(LeanTweenType.easeOutBounce);
            }
        }
    }

    public void SizePlayers()
    {
        foreach(Player player in players)
        {
            if (player.canTransform)
            {
                player.currentScale += scaleAmount;
                player.jumpForce += jumpAmount;

                if (player.currentScale > maxScaleAmount)
                {
                    player.currentScale = minScaleAmount;
                    player.jumpForce = minJumpAmount;
                }

                LeanTween.scale(player.gameObject, new Vector3(player.currentScale, player.currentScale, 1f), 0.5f).setEase(LeanTweenType.easeOutBounce);

                player.transform.localScale = new Vector3(player.currentScale, player.currentScale, 1f);
            }
        }
    }
}
