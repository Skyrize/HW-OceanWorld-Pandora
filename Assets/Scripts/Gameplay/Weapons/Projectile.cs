using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ItemObject))]
public class Projectile : MonoBehaviour
{
    public UnityEvent onHit = new UnityEvent();
    public UnityEvent onTouchWater = new UnityEvent();
    [Header("References")]
    [HideInInspector] protected Ammunition ammunitionAsset = null;
    public GameObject parent;

    public virtual void Start()
    {
        ammunitionAsset = GetComponent<ItemObject>().Item as Ammunition;
    }

    public void Freeze()
    {
        var rb = GetComponent<Rigidbody>();
        rb.detectCollisions = false;
        GetComponent<Collider>().enabled = false;
        rb.velocity = Vector3.zero;
        rb.useGravity = false;
    }

    public IEnumerator Kill()
    {
        yield return null;
        Destroy(this.gameObject);
    }

    public void Hit(GameObject target)
    {
        HealthComponent health = target.GetComponentInParent<HealthComponent>();

        if (parent == health.gameObject) {
            return;
        } else {
            Debug.Log($"parent {parent.name} != target {target.name}");
        }
        if (health) {
            health.ReduceHealth(ammunitionAsset.Damages);
        }
        onHit.Invoke();
        StartCoroutine(Kill());
    }

    private void Update() {
        if (transform.position.y < 0) {
            onTouchWater.Invoke();
            StartCoroutine(Kill());
        }
    }
}
