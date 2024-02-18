using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public enum ColorGroup
{
    GREY,
    RED,
    GREEN,
    BLUE,
    WHITE
}

public enum GameState
{
    PLAY,
    PAUSED,
    LEVEL_COMPLETE
}

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    [Header("Game Settings")]
    public GameState gameState;
    public ColorGroup activeColor;
    public Color[] playerColors = new Color[5];
    public float rotateAmount = 45f;
    public float scaleAmount = 0.25f;
    public float minScaleAmount = 0.5f;
    public float maxScaleAmount = 1.5f;
    public float jumpAmount = 1f;
    public float minJumpAmount = 3.0f;
    public float maxJumpAmount = 7.0f;
    public float timeLimit = 120f;
    public float timeRemaining;
    public float timeElapsed;
    public Transform currentRespawnPointTriangle;
    public Transform currentRespawnPointCircle;

    [Header("Level Objects")]
    public int currentLevel = 1;
    public List<ColorSwitcher> colorSwitchers = new List<ColorSwitcher>();
    public List<Objects> objects = new List<Objects>();
    public List<Rotatable> rotatables = new List<Rotatable>();

    [Header("Player Settings")]
    public Player playerOne;
    public Player playerTwo;
    public List<Player> players = new List<Player>();

    [Header ("Camera")]
    public List<CinemachineVirtualCamera> puzzleVirtualCameras = new List<CinemachineVirtualCamera>();

    [Header("UI")]
    public UIController UI;

    [Header("Audio")]
    public int currentSizeSoundIndex = 9;
    public int currentRotateSoundIndex = 2;

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
        players.Add(playerOne);
        players.Add(playerTwo);

        colorSwitchers.AddRange(FindObjectsOfType<ColorSwitcher>());
        objects.AddRange(FindObjectsOfType<Objects>());
        rotatables.AddRange(FindObjectsOfType<Rotatable>());

        AudioController.instance.PlayLoop("BGM");
    }

    private void Update()
    {
        switch (gameState)
        {
            case GameState.PLAY:
                Time.timeScale = 1.0f;
                break;

            case GameState.PAUSED:
                Time.timeScale = 0.0f;
                break;
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            SwapPlayer();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            FocusCamera(currentLevel);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            UnfocusCamera(currentLevel);
        }

        timeElapsed += Time.deltaTime;

        timeRemaining = timeLimit - timeElapsed;

        UI.UpdateTimer(timeRemaining, timeLimit);

        if (timeRemaining <= 0)
        {
            RestartLevel();
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
        AudioController.instance.PlayIndex(currentRotateSoundIndex);
        currentRotateSoundIndex++;

        if (currentRotateSoundIndex > 6)
        {
            currentRotateSoundIndex = 0;
        }

        foreach(Player player in players)
        {
            if (player.canTransform)
            {
                player.currentRotation += rotateAmount;

                player.currentRotation %= 360f;

                LeanTween.rotateZ(player.spriteChild.gameObject, player.currentRotation, 0.5f).setEase(LeanTweenType.easeOutBounce);
            }
        }

        foreach(Rotatable rotatable in rotatables)
        {
            rotatable.Rotate();
        }
    }

    public void SizePlayers()
    {
        AudioController.instance.PlayIndex(currentSizeSoundIndex);
        currentSizeSoundIndex++;

        if (currentSizeSoundIndex > 11)
        {
            currentSizeSoundIndex = 7;
        }

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

    public void SetActiveColor()
    {
        switch (activeColor)
        {
            case ColorGroup.GREY:
                activeColor = ColorGroup.RED;
                break;
            case ColorGroup.RED:
                activeColor = ColorGroup.GREEN;
                break;
            case ColorGroup.GREEN:
                activeColor = ColorGroup.BLUE;
                break;
            case ColorGroup.BLUE:
                activeColor = ColorGroup.GREY;
                break;
            default:
                activeColor = ColorGroup.GREY;
                break;
        }

        foreach(Player player in players)
        {
            switch (activeColor)
            {
                case ColorGroup.GREY:
                    player.activeColor = activeColor;
                    break;
                case ColorGroup.RED:
                    player.activeColor = activeColor;
                    break;
                case ColorGroup.GREEN:
                    player.activeColor = activeColor;
                    break;
                case ColorGroup.BLUE:
                    player.activeColor = activeColor;
                    break;
                default:
                    player.activeColor = activeColor;
                    break;
            }

            if (player.canTransform)
            {
                player.sr.color = GetColorFromEnum(activeColor);
                player.light.color = GetColorFromEnum(activeColor);
            }

            UpdateObjects();
            AudioController.instance.Play("ColorSwitcher");
        }
    }

    public Color GetColorFromEnum(ColorGroup color)
    {
        switch (color)
        {
            case ColorGroup.GREY:
                return playerColors[0];
            case ColorGroup.RED:
                return playerColors[1];
            case ColorGroup.GREEN:
                return playerColors[2];
            case ColorGroup.BLUE:
                return playerColors[3];
            case ColorGroup.WHITE:
                return playerColors[4];
            default:
                return playerColors[0];
        }
    }

    public void UpdateObjects()
    {
        foreach(Objects obj in objects)
        {
            obj.UpdateCollider();
        }
    }

    public void UpdateRespawnPoint(Transform respawnPointTriangle, Transform respawnPointCircle, float updatedTime)
    {
        currentRespawnPointTriangle = respawnPointTriangle;
        currentRespawnPointCircle = respawnPointCircle;
        timeLimit = updatedTime;
        timeRemaining = timeLimit;
        timeElapsed = 0;
    }

    public void RestartLevel()
    {
        foreach(Player player in players)
        {
            activeColor = ColorGroup.GREY;
            player.transform.position = player.playerIndex == Player.PlayerIndex.ONE ? currentRespawnPointTriangle.position : currentRespawnPointCircle.position;
            player.isMovable = true;
            player.canTransform = true;
            player.rb.gravityScale = 1.0f;
            player.rb.velocity = Vector2.zero;
            player.currentScale = 1.0f;
            player.jumpForce = 5.0f;
            player.currentRotation = 0.0f;
            player.sr.color = GetColorFromEnum(activeColor);
            player.light.color = GetColorFromEnum(activeColor);
            player.transform.localScale = new Vector3(player.currentScale, player.currentScale, 1f);
            player.spriteChild.transform.localEulerAngles = Vector3.zero;
        }

        if (currentLevel == 1)
        {
            timeLimit = 120f;
        }
        if (currentLevel == 2)
        {
            timeLimit = 180f;
        }
        if (currentLevel == 3)
        {
            timeLimit = 240f;
        }

        timeRemaining = timeLimit;
        timeElapsed = 0;
    }

    public void FocusCamera(int levelNumber)
    {
        foreach(CinemachineVirtualCamera vcam in puzzleVirtualCameras)
        {
            if (vcam.Priority == 11)
            {
                vcam.Priority = 9;
            }
        }

        puzzleVirtualCameras[levelNumber - 1].Priority = 11;
    }

    public void UnfocusCamera(int levelNumber)
    {
        puzzleVirtualCameras[levelNumber - 1].Priority = 9;
    }
}
