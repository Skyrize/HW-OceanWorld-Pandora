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

    // Update is called once per frame
    void Update()
    {

    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(TargetType))
            collision.gameObject
                .GetComponent<IHitable>()
                .hitBy(this);
    }
}

public enum ProjectileType
{
    CANONBALL,
}
