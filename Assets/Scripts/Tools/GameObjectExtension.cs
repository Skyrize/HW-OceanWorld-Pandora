using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectExtension {
    public static void SetColor(this GameObject gameObject, Color color)
    {
        MeshRenderer[] meshes = gameObject.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer mesh in meshes)
        {
            mesh.material.color = color;
        }
    }
    public static void SetColors(this GameObject gameObject, Color[] colors)
    {
        MeshRenderer[] meshes = gameObject.GetComponentsInChildren<MeshRenderer>();
        if (meshes.Length != colors.Length)
            throw new System.Exception("Meshes and colors have to match in length (" + gameObject.name + "). Colors : " + colors.Length + " / meshes : " + meshes.Length);
        for (int i = 0; i != meshes.Length; i++) {
            meshes[i].material.color = colors[i];
        }
    }
    public static Color[] GetColors(this GameObject gameObject)
    {
        MeshRenderer[] meshes = gameObject.GetComponentsInChildren<MeshRenderer>();
        if (meshes.Length == 0)
            return null;
        Color[] colors = new Color[meshes.Length];

        for (int i = 0; i != meshes.Length; i++) {
            colors[i] = meshes[i].material.color;
        }
        return colors;
    }
}
