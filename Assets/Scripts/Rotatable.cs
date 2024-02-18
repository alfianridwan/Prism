using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotatable : MonoBehaviour
{
    private Transform pivot;
    public bool isOpposite = false;

    public void Start()
    {
        pivot = transform.parent;
    }

    public void Rotate()
    {
        if (isOpposite)
        {
            transform.RotateAround(pivot.position, Vector3.forward, -1 * GameController.Instance.rotateAmount);
        }
        else
        {
            transform.RotateAround(pivot.position, Vector3.forward, GameController.Instance.rotateAmount);
        }

        // set z position to 0
        Vector3 pos = transform.position;
        pos.z = 0;
        transform.position = pos;
    }
}
