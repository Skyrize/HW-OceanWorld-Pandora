using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtension {
    public static Transform ClearChilds(this Transform transform)
    {
        int count = transform.childCount;
        for (int i = count - 1; i >= 0; i--) {
            if (Application.isPlaying) {
                GameObject.Destroy(transform.GetChild(i).gameObject);
            } else {
                GameObject.DestroyImmediate(transform.GetChild(i).gameObject);
            }
        }
        return transform;
    }
}
