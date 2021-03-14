using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    private static Camera main = Camera.main;
    public static Plane seaLevel = new Plane(Vector3.up, Vector3.zero);

    public static Camera Main 
    { 
        get 
        { 
            if (main == null) 
                main = Camera.main; 
            return main; 
        } 
    }


    public static Vector3 MousePosition
    {
        get
        {
            Physics.Raycast(Main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit);
            return hit.point;
        }
    }

    public static Vector3 MousePositionOcean
    {
        get
        {
            Ray ray = Main.ScreenPointToRay(Input.mousePosition);
            float distance;
            Utils.seaLevel.Raycast(ray, out distance);
            Debug.DrawRay(ray.origin, ray.direction * distance, Color.red, 1);
            return ray.origin + ray.direction * distance;
        }
    }

    public sealed class Tags
    {
        public static readonly string PLAYER = "Player";
        public static readonly string ENNEMY = "Ennemy";
        public static readonly string PROJECTILE_CONTAINER = "ProjectileContainer";
    }
}
