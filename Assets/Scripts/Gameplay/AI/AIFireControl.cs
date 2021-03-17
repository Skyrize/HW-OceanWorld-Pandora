using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class AIFireControl : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AIVision vision;
    [SerializeField] private Weaponry weaponery;
    public bool shouldFire()
    {
        return vision.lastKnownPlayerPos.HasValue &&
            vision.timeSinceLastSeen == 0;
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

        foreach (WeaponManager weapon in weaponery.weapons)
        {
            if (!weapon.isActiveAndEnabled || !weapon.canShoot())
            {
                continue;
            }
            Vector3 originPos = weapon.spawnPoint.transform.position;
            originPos.y = 0f;
            Vector3 originForward = weapon.spawnPoint.transform.forward;
            List<Vector3> testedDirection = new List<Vector3>() {
                originForward,
                Quaternion.AngleAxis(-weapon.maxAngle, Vector3.up) * originForward,
                Quaternion.AngleAxis(weapon.maxAngle, Vector3.up) * originForward,
            };
            foreach (Vector3 direction in testedDirection)
            {
                if (Physics.Raycast(originPos, direction, out hit, weapon.MaxRange) &&
                    isTarget(hit.collider.gameObject))
                {
                    Fire(hit.collider.gameObject);
                    didFire = true;
                    break;
                }
            }
        }
        return didFire;
    }

    public void Fire(GameObject target)
    {
        weaponery.ShootAt(target.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldFire())
        {
            handleFire();
        }
    }
}
