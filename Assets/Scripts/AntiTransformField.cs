using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiTransformField : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player enter anti-transform field");
            collision.gameObject.GetComponent<Player>().canTransform = false;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player exit anti-transform field");
            collision.gameObject.GetComponent<Player>().canTransform = true;
        }
    }
}
