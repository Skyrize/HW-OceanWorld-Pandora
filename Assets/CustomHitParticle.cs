using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomHitParticle : MonoBehaviour
{
    public ColliderEvent evt = null;
    ParticleSystem ps = null;

    private void Start() {
        ps = GetComponent<ParticleSystem>();
    }

    public void Hit()
    {
        transform.position = evt.LastCollisionLocation;
        ps.Play();
    }
}
