using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public enum FireSide
{
    NONE,
    LEFT,
    RIGHT,
    FRONT
}

[System.Serializable] public class OnFire : UnityEvent<FireSide, Vector3>
{
}

struct FireInfo
{
    public FireSide side;
    public Vector3 direction;
}

public class AIFireControl : MonoBehaviour
{
    [SerializeField] public float attackRange = 20f;
    public Dictionary<FireSide, bool> availableFireSide { get; private set; } = new Dictionary<FireSide, bool>();
    public OnFire onFire;

    [Header("References")]
    [SerializeField] private AIVision vision;
    [SerializeField] private Weaponry weaponery;

    private void Start()
    {
        availableFireSide.Add(FireSide.FRONT, false);
        availableFireSide.Add(FireSide.LEFT, false);
        availableFireSide.Add(FireSide.RIGHT, false);
        foreach (FireSide fireSide in weaponery.getShootableFireSide())
        {
            availableFireSide[fireSide] = true;
        }
    }

    public bool IsInAttackRange()
    {
        return vision.lastKnownPlayerPos.HasValue &&
            vision.timeSinceLastSeen == 0 &&
            Vector3.Distance(vision.lastKnownPlayerPos.Value, transform.position) <= attackRange;
    }

    public bool isTarget(GameObject target)
    {
        return target.CompareTag("Player") ||
            (target.transform.parent && target.transform.parent.CompareTag("Player")) ||
            (target.transform.parent.parent && target.transform.parent.parent.CompareTag("Player"));
    }

    public bool handleFire()
    {
        RaycastHit hit;
        bool didFire = false;

        List<FireInfo> fireInfos = new List<FireInfo>() {
            new FireInfo() { side = FireSide.LEFT, direction= -transform.right },
            new FireInfo() { side = FireSide.RIGHT, direction= transform.right },
            new FireInfo() { side = FireSide.FRONT, direction= transform.forward }
        };

        fireInfos.ForEach(fireInfo =>
        {
            if (availableFireSide[fireInfo.side] &&
                Physics.Raycast(transform.position, fireInfo.direction, out hit, attackRange) &&
                isTarget(hit.collider.gameObject)
            )
            {
                Fire(fireInfo.side, hit.collider.gameObject);
                didFire = true;
            }
        });
        return didFire;
    }

    public void Fire(FireSide side, GameObject target)
    {
        onFire?.Invoke(side, target.transform.position);
        weaponery.ShootAt(target.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        handleFire();
    }
}
