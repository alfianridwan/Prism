using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Objects : MonoBehaviour
{
    public enum ColorGroup
    {
        GREY,
        RED,
        GREEN,
        BLUE,
        WHITE
    }

    public Collider2D collider;
    public ColorGroup color;


    public void Start()
    {
        Collider2D collider = GetComponent<Collider2D>();
        if (collider == null)
        {
            Debug.LogError("No Collider2D found on this GameObject!");
        }

        UpdateCollider();
    }

    public void UpdateCollider()
    {
        switch(color)
        {
            case ColorGroup.GREY:
                collider.isTrigger = false;
                break;
            case ColorGroup.RED:
                collider.isTrigger = false;
                break;
            case ColorGroup.GREEN:
                collider.isTrigger = false;
                break;
            case ColorGroup.BLUE:
                collider.isTrigger = false;
                break;
            case ColorGroup.WHITE:
                collider.isTrigger = true;
                break;
        }
    }
}
