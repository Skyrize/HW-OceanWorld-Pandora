using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Ammunition")]
public class Ammunition : Item
{
    [Header("Settings")]
    public float damages = 1.0f;
}
