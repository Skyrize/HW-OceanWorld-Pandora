using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Ammunition")]
public class Ammunition : Item
{
    [Header("Settings")]
    [SerializeField] protected float damages = 1.0f;
    public float Damages => damages;
}
