using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticColliderBound : MonoBehaviour
{
    public BoxCollider targetCollider;

    public void Rebound() {
        if (!targetCollider) targetCollider = GetComponent<BoxCollider>();
        Bounds bounds = new Bounds(transform.position, Vector3.zero);
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        Renderer rootRenderer = GetComponent<Renderer>();

        if (rootRenderer)
            bounds.Encapsulate(rootRenderer.bounds);
        foreach (Renderer renderer in renderers)
        {
            // Bounds childBounds = renderer.bounds;

            // childBounds.center = renderer.transform.TransformPoint(childBounds.center);
            Debug.Log("Encapsulate " + renderer.gameObject.name);
            bounds.Encapsulate(renderer.bounds);
        }
        targetCollider.center = bounds.center - transform.position;
        targetCollider.size = bounds.size;
    }
}
