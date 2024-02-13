using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isPlayerOne = true;
    public float speed = 5.0f;
    public float jumpForce = 5.0f;
    public bool isJumping = false;

    void Update()
    {
        if (isPlayerOne)
        {
            Move(KeyCode.A, KeyCode.D, KeyCode.W, KeyCode.S);
        }
        else
        {
            Move(KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.DownArrow);
        }
    }

    void Move(KeyCode left, KeyCode right, KeyCode up, KeyCode down)
    {
        if (Input.GetKey(left))
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        if (Input.GetKey(right))
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        if (Input.GetKeyDown(up) && !isJumping)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJumping = true;
        }
        if (Input.GetKeyDown(down))
        {
            Interact();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }

    public virtual void Interact()
    {
        Debug.Log("Interacting with object");
    }
}
