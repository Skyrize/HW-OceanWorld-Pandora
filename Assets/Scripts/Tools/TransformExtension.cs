using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtension {
    public static Transform ClearChilds(this Transform transform)
    {
        foreach (Transform child in transform) {
            GameObject.Destroy(child.gameObject);
        }
        return transform;
    }
}
