using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Weapon")]
public class Weapon : Item
{
    [Header("Settings")]
    public float shotPerSecond = 2f;
    public float power = 100f;
    [Header("References")]
    public Ammunition ammunitionAsset;

}
