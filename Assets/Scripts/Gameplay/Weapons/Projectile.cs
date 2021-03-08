using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 Origin;
    public Vector3 Target;
    public ProjectileType Type;

    public virtual void Start()
    {
        transform.position = Origin;
    }

    protected virtual void OnHit(Collider collider)
    {
        IHitable hitable = collider.gameObject.GetComponent<IHitable>();
        if (hitable != null)
            collider.gameObject
                .GetComponent<IHitable>()
                .HitBy(this);
    }
}

public enum ProjectileType
{
    CANONBALL,
    BULLET,
}
