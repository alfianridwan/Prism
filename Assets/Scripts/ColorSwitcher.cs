using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ColorSwitcher : MonoBehaviour
{
    public bool interactable = false;
    private Collider2D collider;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player enter color switcher");
            collision.gameObject.GetComponent<Player>().swapInteractive = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player exit color switcher");
            collision.gameObject.GetComponent<Player>().swapInteractive = false;
        }
    }
}
