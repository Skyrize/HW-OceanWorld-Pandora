using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Weapon")]
public class Weapon : Item
{
    [Header("Settings")]
    [SerializeField] protected float shotsPerSecond = 2f;
    public float ShotsPerSecond => shotsPerSecond;
    [SerializeField] protected float power = 100f;
    public float Power => power;
    [Header("References")]
    [SerializeField] protected Ammunition ammunitionAsset = null;
    public Ammunition AmmunitionAsset => ammunitionAsset;

}
