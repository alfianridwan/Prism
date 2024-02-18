using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Objects : MonoBehaviour
{
    private Collider2D collider;
    private SpriteRenderer sr;
    public ColorGroup color;

    public void Start()
    {
        collider = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();

        if (collider == null)
        {
            Debug.LogError("No Collider2D found on this GameObject!");
        }

        UpdateCollider();

        sr.color = GameController.Instance.GetColorFromEnum(color);
    }

    public void UpdateCollider()
    {
        bool isTrigger = false;

        if (GameController.Instance.activeColor == color)
        {
            isTrigger = true;
        }

        if (color == ColorGroup.GREY)
        {
            isTrigger = false;
        }

        if (color == ColorGroup.WHITE)
        {
            isTrigger = true;
        }

        collider.isTrigger = isTrigger;
    }

    public void OnValidate()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.color = GameController.Instance.GetColorFromEnum(color);
    }

    // a function that rotates the object by GameController rotate amount


}
