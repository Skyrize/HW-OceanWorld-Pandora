using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    private static Camera main = Camera.main;

    private static Camera Main()
    {
        if (main == null)
            main = Camera.main;
        return main;
    }

    public static Vector3 MousePosition
    {
        get
        {
            Physics.Raycast(Main().ScreenPointToRay(Input.mousePosition), out RaycastHit hit);
            return hit.point;
        }
    }

    public static Vector3 Copy(Vector3 v)
    {
        return new Vector3(
            v.x,
            v.y,
            v.z);
    }

    public static GameObject FindGameObject(string tag) => GameObject.FindGameObjectWithTag(tag);
    public static GameObject[] FindGameObjects(string tag) => GameObject.FindGameObjectsWithTag(tag);

    public sealed class Tags
    {
        public static readonly string PLAYER = "Player";
        public static readonly string ENNEMY = "Ennemy";
    }
}
