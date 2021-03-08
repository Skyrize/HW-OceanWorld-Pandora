using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canonball : Projectile
{
    public GameObject explosionPrefab;
    
    private Rigidbody body;

    private float maxRange;
    private readonly float baseVelocity = 10f;
    private readonly float maxSideAngle = 45f;

    public override void Start()
    {
        base.Start();
        Type = ProjectileType.CANONBALL;

        maxRange = MaxRange;
        body = GetComponent<Rigidbody>();

        transform.LookAt(Target);
        transform.Rotate(Vector3.right * ZRotatingAngle);

        body.velocity = transform.TransformDirection(Vector3.forward * baseVelocity);
    }

    private float ZRotatingAngle
    {
        get
        {
            if (IsInRange)
                return -45f;

            return .5f * Mathf.Asin(Gravity * Range / V2) * Mathf.Rad2Deg;
        }
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

    private bool IsInRange => Range > maxRange;
    private float MaxRange => Mathf.Abs(Mathf.Pow(baseVelocity, 2) / Physics.gravity.y);

    private float Range => Vector3.Distance(Origin, Target);
    private float Gravity => Physics.gravity.y;

    private float V2 => Mathf.Pow(baseVelocity, 2);
}
