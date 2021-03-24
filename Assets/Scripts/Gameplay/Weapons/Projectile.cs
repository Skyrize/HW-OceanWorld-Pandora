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
    [HideInInspector] public GameObject parent;

    public float? damages = null;

    private bool touchedWater = false;

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

        if (health) {
            if (parent == health.gameObject) {
                return;
            }
            health.ReduceHealth(damages.HasValue ? damages.Value : ammunitionAsset.Damages);
        }
        onHit.Invoke();
        StartCoroutine(Kill());
    }

    private void Update() {
        if (transform.position.y < 0 && !touchedWater)
        {
            touchedWater = true;
            onTouchWater.Invoke();
            StartCoroutine(Kill());
        }
    }
}
