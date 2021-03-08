using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    private static Camera main = Camera.main;

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

    public sealed class Tags
    {
        public static readonly string PLAYER = "Player";
        public static readonly string ENNEMY = "Ennemy";
        public static readonly string PROJECTILE_CONTAINER = "ProjectileContainer";
    }
}
