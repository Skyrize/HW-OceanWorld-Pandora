using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponsManager : MonoBehaviour, IHitable
{
    public WeaponManager canon;

    public void hitBy(Projectile p)
    {
        throw new System.NotImplementedException();
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
