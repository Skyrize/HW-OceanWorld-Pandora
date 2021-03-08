using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Should get ridden of
/// </summary>
public class PlayerWeaponsManager : MonoBehaviour, IHitable
{
    public WeaponManager canon;

    public void HitBy(Projectile p)
    {
        print($"I, Player, was hit by a { p.Type }");
    }

    void Update()
    {
        if (Input.GetButton("Fire1"))
            canon.ShootAt(Utils.MousePosition);
    }
}
