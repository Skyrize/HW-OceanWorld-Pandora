using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weaponry : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] protected bool loadFromChilds = true;
    [Header("References")]
    [SerializeField] public List<WeaponManager> weapons;

    public void LoadWeaponsFromChilds()
    {
        var weapons = GetComponentsInChildren<WeaponManager>();

        foreach (var weapon in weapons)
        {
            this.weapons.Add(weapon);
        }
    }

    private void Awake() {
        if (loadFromChilds) {
            LoadWeaponsFromChilds();
        }
    }

    public List<FireSide> getShootableFireSide()
    {
        // TODO use weapon angles to define the shootable side
        return new List<FireSide>() { FireSide.FRONT, FireSide.LEFT, FireSide.RIGHT };
    }

    public void ShootAt(Vector3 target)
    {
        // Debug.Log($"Shooting at {target}");
        foreach (var weapon in weapons)
        {
            // Debug.Log("Shooting");
            weapon.Use(target);
        }
    }

}
