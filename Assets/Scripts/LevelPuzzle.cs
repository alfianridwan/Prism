using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPuzzle : MonoBehaviour
{
    public bool isTriangle = true;
    public LevelComplete levelComplete;
    public float rotationRequired;
    public float scaleRequired;
    public ColorGroup colorRequired;
    public bool rotationComplete = false;
    public bool scaleComplete = false;
    public bool colorComplete = false;
    public bool fullComplete = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();

            if (player.currentRotation == rotationRequired)
            {
                rotationComplete = true;
            }
            else
            {
                rotationComplete = false;
            }

            if (player.currentScale == scaleRequired)
            {
                scaleComplete = true;
            }
            else
            {
                scaleComplete = false;
            }

            if (player.activeColor == colorRequired)
            {
                colorComplete = true;
            }
            else
            {
                colorComplete = false;
            }

            if (rotationComplete && scaleComplete && colorComplete && !fullComplete)
            {
                if (player.playerIndex == Player.PlayerIndex.ONE && isTriangle)
                {
                    player.transform.position = transform.position;
                    player.isMovable = false;
                    player.canTransform = false;
                    player.rb.gravityScale = 0.0f;
                    player.rb.velocity = Vector2.zero;
                    fullComplete = true;
                    levelComplete.puzzleTriangleComplete = true;
                    AudioController.instance.Play("TriangleComplete");
                }

                if (player.playerIndex == Player.PlayerIndex.TWO && !isTriangle)
                {
                    player.transform.position = transform.position;
                    player.isMovable = false;
                    player.canTransform = false;
                    player.rb.gravityScale = 0.0f;
                    player.rb.velocity = Vector2.zero;
                    fullComplete = true;
                    levelComplete.puzzleCircleComplete = true;
                    AudioController.instance.Play("CircleComplete");
                }
            }
        }
    }
}
