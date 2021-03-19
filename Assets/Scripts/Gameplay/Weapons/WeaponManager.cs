﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ItemObject))]
public class WeaponManager : Post
{
    public float maxAngle = 30f;
    [Header("TMP_OldCannonball shoot")]
    public bool TMP_followTarget = true;
    private float maxRange;
    private readonly float baseVelocity = 10f;
    // private readonly float maxSideAngle = 45f;

    private bool IsInRange => Range > maxRange;
    public float MaxRange => Mathf.Abs(Mathf.Pow(baseVelocity, 2) / Physics.gravity.y);
    private Vector3 Target = Vector3.zero;

    private float Range => Vector3.Distance(spawnPoint.position, Target);
    private float Gravity => Physics.gravity.y;

    private float V2 => Mathf.Pow(baseVelocity, 2);


    private float ZRotatingAngle
    {
        get
        {
            if (IsInRange)
                return -45f;

            return .5f * Mathf.Asin(Gravity * Range / V2) * Mathf.Rad2Deg;
        }
    }

    [SerializeField] protected bool canRotate = false;
    [SerializeField] protected float rotationSpeed = 5f;
    [SerializeField] public ErrorEvent onMissingResource = new ErrorEvent();
    [SerializeField] public UnityEvent onShoot = new UnityEvent();
    [Header("References")]
    [HideInInspector] protected Weapon weaponAsset;
    [SerializeField] public Transform spawnPoint = null;
    [Header("Runtime")]
    [SerializeField] bool reloaded = true;
    [HideInInspector] protected Inventory inventory;
    private WaitForSeconds reloadTimer = null;
    private Quaternion baseRotation = Quaternion.identity;
    private Vector3 baseForward = Vector3.zero;

    private void Awake() {
        weaponAsset = GetComponent<ItemObject>().Item as Weapon;
        if (!weaponAsset)
            throw new System.Exception($"ItemObject on WeaponManager '{gameObject.name}' has an invalid item (null or not Weapon).");
        reloadTimer = new WaitForSeconds(1.0f / weaponAsset.ShotsPerSecond);
    }

    Camera cam = null;  // cached because Camera.main is slow, so we only call it once.
    void Start()
    {
        cam = Camera.main;
        baseRotation = transform.rotation;
        baseForward = spawnPoint.forward;
    }

    IEnumerator Reload()
    {
        reloaded = false;
        yield return reloadTimer;
        reloaded = true;
        //TODO : add event for sound binding
    }

    public void TMP_FollowTargetShoot(GameObject projectile, Vector3 target)
    {
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
        Target = target;
        maxRange = MaxRange;

        projectile.transform.LookAt(Target);
        projectile.transform.Rotate(Vector3.right * ZRotatingAngle);

        projectileRb.velocity = projectile.transform.TransformDirection(Vector3.forward * baseVelocity);
    }

    public void TMP_DirectShoot(GameObject projectile, Vector3 target)
    {
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
        Vector3 forward = (target - spawnPoint.position).normalized;

        projectileRb.AddForce(forward * weaponAsset.Power, ForceMode.Impulse);
    }

    bool UseAmmo()
    {
        if (!inventory) {
            inventory = GetComponentInParent<InventoryHolder>().inventory;
        }
        InventoryStorage stored = inventory.GetStoredItem(weaponAsset.AmmunitionAsset);

        if (stored == null)
        {
            onMissingResource.Invoke($"Your {weaponAsset.Name} is out of {weaponAsset.AmmunitionAsset.Name} !");
            return false;
        }
        inventory.Remove(stored.item);
        return true;
    }

    public bool canShoot()
    {
        return reloaded && (!inventory || inventory.GetStoredItem(weaponAsset.AmmunitionAsset) != null);
    }

    public bool isValidTarget(Vector3 target)
    {
        Vector3 shootVector = target - spawnPoint.position;
        float angle = Vector3.Angle(shootVector, baseForward);
        bool isValidAngle = angle < maxAngle || angle > 360f - maxAngle;
        return shootVector.magnitude < MaxRange && isValidAngle;
    }

    protected void ShootAt(Vector3 target)
    {
        if (!reloaded || !isValidTarget(target) || !UseAmmo())
        {
            return;
        }
        var projectile = Instantiate(weaponAsset.AmmunitionAsset.Prefab,
            spawnPoint.position,
            spawnPoint.rotation);

        if (TMP_followTarget) {
            TMP_FollowTargetShoot(projectile, target);
        } else {
            TMP_DirectShoot(projectile, target);
        }
        onShoot.Invoke();
        StartCoroutine(Reload());
    }

    override protected void _Use(Vector3 target)
    {
        ShootAt(target);
    }

    public void RotateToward(Vector3 target)
    {
        if (!canRotate || !employee)
            return;
        Vector3 forward = target - transform.position;
        forward.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(forward, Vector3.up);

        if (Quaternion.Angle(targetRotation, baseRotation) <= maxAngle) {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        } else {
            if (transform.InverseTransformDirection(forward).x < 0)
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(-maxAngle, Vector3.up), Time.deltaTime * rotationSpeed);
            else
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(maxAngle, Vector3.up), Time.deltaTime * rotationSpeed);
        }
    }

}