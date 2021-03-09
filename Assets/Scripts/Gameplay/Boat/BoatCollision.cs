using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatCollision : MonoBehaviour
{
    public float damageOnCollision = 5f;
    public HealthComponent stats;
    private void OnCollisionEnter(Collision collision)
    {
        stats.ReduceHealth(damageOnCollision);
    }
}
