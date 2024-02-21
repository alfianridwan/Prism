using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum PlayerIndex
    {
        ONE,
        TWO,
        TEMP_ONE,
        TEMP_TWO
    }

    public PlayerIndex playerIndex;
    public ColorGroup activeColor;

    public float speed = 5.0f;
    public float jumpForce = 5.0f;
    public float currentScale = 1.0f;
    public float groundCheckRadius = 0.01f;
    public float currentRotation = 0.0f;
    public bool isMovable = true;
    public bool isGrounded = true;
    public bool canTransform = true;
    public bool swapInteractive = false;
    public LayerMask whatIsGround;
    public Transform checkSphere;

    public GameObject spriteChild;
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public Collider2D collider;
    public UnityEngine.Rendering.Universal.Light2D light;

    void Start()
    {
        sr = spriteChild.GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        isMovable = true;
        activeColor = ColorGroup.GREY;
    }

    void Update()
    {
        if (GameController.Instance.gameState != GameState.PLAY) return;

        if (playerIndex == PlayerIndex.ONE)
        {
            Move(KeyCode.A, KeyCode.D, KeyCode.W, KeyCode.S);
        }
        else
        {
            Move(KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.DownArrow);
        }

        isGrounded = Physics2D.OverlapCircle(checkSphere.position, groundCheckRadius, whatIsGround);

        // force player transform z to 0
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    void Move(KeyCode left, KeyCode right, KeyCode up, KeyCode down)
    {
        if (isMovable)
        {
            if (Input.GetKey(left))
            {
                transform.Translate(Vector3.left * speed * Time.deltaTime);
            }
            if (Input.GetKey(right))
            {
                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }
            if (Input.GetKeyDown(up) && isGrounded)
            {
                GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }

        if (Input.GetKeyDown(down))
        {
            Interact();
        }
    }

    public void Interact()
    {
        if (swapInteractive)
        {
            GameController.Instance.SetActiveColor();
        }

        if (playerIndex == PlayerIndex.ONE)
        {
            if (!swapInteractive)
                GameController.Instance.RotatePlayers();
        }
        else
        {
            if (!swapInteractive)
                GameController.Instance.SizePlayers();
        }
    }

    public void ChangePlayerIndex(PlayerIndex newPlayerIndex) => playerIndex = newPlayerIndex;

    public void SwapPlayer()
    {
        if (playerIndex == PlayerIndex.ONE)
        {
            ChangePlayerIndex(PlayerIndex.TWO);
        }
        else
        {
            ChangePlayerIndex(PlayerIndex.ONE);
        }
    }
}
