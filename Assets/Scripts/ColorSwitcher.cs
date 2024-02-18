using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ColorSwitcher : MonoBehaviour
{
    public enum ColorGroup
    {
        GREY,
        RED,
        GREEN,
        BLUE,
        WHITE
    }

    public ColorGroup activeColor;
    public Collider2D collider;
    public bool interactable = false;

    public void Interact()
    {
        if (!interactable) return;

        // cycle through the activeColor from GREY to WHITE each time this object is being interated

    }
}
