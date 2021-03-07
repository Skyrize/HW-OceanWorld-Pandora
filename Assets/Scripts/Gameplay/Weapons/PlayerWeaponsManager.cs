using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponsManager : MonoBehaviour, IHitable
{
    public WeaponManager canon;

    public void HitBy(Projectile p)
    {
        print($"I, Player, was hit by a { p.Type }");
    }

    void Start()
    {
        canon.TargetType = Utils.Tags.ENNEMY;
    }

    void Update()
    {
        if (Input.GetButton("Fire1"))
            canon.ShootAt(Utils.MousePosition);
    }
}
