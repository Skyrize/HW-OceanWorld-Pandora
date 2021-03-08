using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile
{
    public override void Start()
    {
        base.Start();
        Type = ProjectileType.BULLET;

        if (Physics.Linecast(Origin, Target, out RaycastHit hit))
            OnHit(hit.collider);
    }
}
