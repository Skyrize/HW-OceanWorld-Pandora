using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ItemObject))]
public class Projectile : MonoBehaviour
{
    [Header("References")]
    [HideInInspector] protected Ammunition ammunitionAsset = null;
    
    public virtual void Start()
    {
        ammunitionAsset = GetComponent<ItemObject>().Item as Ammunition;
    }



    public void Hit(GameObject target)
    {
        HealthComponent health = target.GetComponentInParent<HealthComponent>();
        HealthComponent ignoreParentTricks = GetComponentInParent<HealthComponent>();

        if (health == ignoreParentTricks)
            return;
        if (health) {
            health.ReduceHealth(ammunitionAsset.Damages);
        }
        GetComponent<Rigidbody>().detectCollisions = false;
        GetComponent<Collider>().enabled = false;
        Destroy(gameObject);
    }

    private void Update() {
        //TODO : remove (replaced by hit water + animation)
        if (transform.position.y < -10) {
            Destroy(gameObject);
        }
    }
}
