using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Weapon")]
public class Weapon : Item
{
    [Header("Settings")]
    [SerializeField] protected int prout = 0;
}
