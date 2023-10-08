using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableBlock : MonoBehaviour
{
    public Vector3 EndPosition;

    public void Move()
    {
        transform.localPosition = EndPosition;
    }
}
