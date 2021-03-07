using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 Origin { get; set; }
    public Vector3 Target { get; set; }
    public string TargetType { get; set; }
    public ProjectileType Type { get; set; }

    public virtual void Start()
    {
        if (Origin == null) Destroy(gameObject);
        else transform.position = Origin;

        if (Target == null)
            Destroy(gameObject);

        if (TargetType == null)
            Destroy(gameObject);
    }

    protected virtual void OnHit(Collider collider)
    {
        if (collider.gameObject.CompareTag(TargetType))
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
