using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canonball : Projectile
{
    public GameObject explosionPrefab;
    private static readonly float speed = 1;
    
    private Rigidbody body;

    public override void Start()
    {
        base.Start();
        Type = ProjectileType.CANONBALL;

        body = GetComponent<Rigidbody>();

        body.velocity = speed
            * ComputeParabolic(Origin, Target);
    }

    protected void OnCollisionEnter(Collision collision)
    {
        base.OnHit(collision.collider);

        Instantiate(explosionPrefab,
            transform.position,
            transform.rotation,
            transform.parent)
            //.SetActive(true)
            ;

        Destroy(gameObject);
    }

    protected Vector3 ComputeParabolic(Vector3 origin, Vector3 target)
    {
        var distance = target - origin;

        var distanceXZ = distance;
        distanceXZ.y = 0f;

        var result = distanceXZ.normalized * distanceXZ.magnitude;
        result.y = distance.y + .5f * Mathf.Abs(Physics.gravity.y);

        return result;
    }
}
