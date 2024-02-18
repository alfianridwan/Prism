using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Transform currentRespawnPointTriangle;
    public Transform currentRespawnPointCircle;
    public LevelComplete levelComplete;
    public bool triggered = false;
    public float newTimeLimit;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !triggered && levelComplete.puzzleCompleted)
        {
            triggered = true;
            GameController.Instance.UpdateRespawnPoint(currentRespawnPointTriangle, currentRespawnPointCircle, newTimeLimit);
        }
    }
}
