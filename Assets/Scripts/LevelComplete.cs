using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LevelComplete : MonoBehaviour
{
    public int levelNumber;
    public GameObject[] objectsToEnable;
    public GameObject[] objectsToDisable;
    public SpriteRenderer backgroundFade;
    public CinemachineVirtualCamera vcam;
    public bool puzzleCompleted = false;

    public bool puzzleTriangleComplete = false;
    public bool puzzleCircleComplete = false;

    void Update()
    {
        if (puzzleTriangleComplete && puzzleCircleComplete)
        {

            if (puzzleCompleted == false)
            {
                foreach (GameObject obj in objectsToEnable)
                {
                    obj.SetActive(true);
                }

                foreach (GameObject obj in objectsToDisable)
                {
                    obj.SetActive(false);
                }

                foreach (Player player in GameController.Instance.players)
                {
                    player.isMovable = true;
                    player.canTransform = true;
                    player.collider.isTrigger = false;
                    player.rb.gravityScale = 1.0f;
                }

                StartCoroutine(FadeBackground());
            }
        }
    }

    public IEnumerator FadeBackground()
    {
        puzzleCompleted = true;

        GameController.Instance.currentLevel++;

        vcam.Priority = 15;
        yield return new WaitForSeconds(2.0f);

        LeanTween.alpha(backgroundFade.gameObject, 1.0f, 1.0f);

        AudioController.instance.Play("LevelComplete");
        // GameController.Instance.gameState = GameState.MAINMENU;

        yield return new WaitForSeconds(3f);

        vcam.Priority = 9;

        yield return new WaitForSeconds(1f);

        if (GameController.Instance.currentLevel == 4)
        {
            GameController.Instance.UI.ShowGameComplete();
        }
    }
}
