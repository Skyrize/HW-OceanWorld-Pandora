using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformEvent : MonoBehaviour
{
    public void SetY(float y)
    {
        Vector3 pos = transform.position;
        pos.y = y;
        transform.position = pos;
        transform.rotation = Quaternion.identity;
    }
}
