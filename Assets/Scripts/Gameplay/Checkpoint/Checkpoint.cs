using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Checkpoint : MonoBehaviour
{
    public BoxCollider boxCollider;

    private void OnDrawGizmos()
    {
        if (!boxCollider)
            return;

        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawCube(transform.position + boxCollider.center, boxCollider.size);
    }
}