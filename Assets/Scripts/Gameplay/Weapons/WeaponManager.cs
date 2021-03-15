using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ItemObject))]
public class WeaponManager : Post
{
    [Header("TMP_OldCannonball shoot")]
    public bool TMP_followTarget = true;
    private float maxRange;
    private readonly float baseVelocity = 10f;
    // private readonly float maxSideAngle = 45f;

    private bool IsInRange => Range > maxRange;
    private float MaxRange => Mathf.Abs(Mathf.Pow(baseVelocity, 2) / Physics.gravity.y);
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

    [SerializeField] public ErrorEvent onMissingResource = new ErrorEvent();
    [Header("References")]
    [HideInInspector] protected Weapon weaponAsset;
    [SerializeField] protected Transform spawnPoint = null;
    [Header("Runtime")]
    [SerializeField] bool reloaded = true;
    [HideInInspector] protected Inventory inventory;
    private WaitForSeconds reloadTimer = null;

    private void Awake() {
        weaponAsset = GetComponent<ItemObject>().Item as Weapon;
        if (!weaponAsset)
            throw new System.Exception($"ItemObject on WeaponManager '{gameObject.name}' has an invalid item (null or not Weapon).");
        reloadTimer = new WaitForSeconds(1.0f / weaponAsset.ShotsPerSecond);
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
        
        projectileRb.AddForce(spawnPoint.forward * weaponAsset.Power, ForceMode.Impulse);
    }

    bool UseAmmo()
    {
        if (!inventory) {
            inventory = GetComponentInParent<InventoryHolder>().inventory;
        }
        InventoryStorage stored = inventory.GetStoredItem(weaponAsset.AmmunitionAsset);

        if (stored == null)
            return false;
        inventory.Remove(stored.item);
        return true;
    }

    protected void ShootAt(Vector3 target)
    {
        //TODO : reject shoot if angle is too large
        if (!reloaded)
            return;
        if (!UseAmmo()) {
            onMissingResource.Invoke($"Your {weaponAsset.Name} is out of {weaponAsset.AmmunitionAsset.Name} !");
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
        StartCoroutine(Reload());
    }

    override protected void _Use(Vector3 target)
    {
        ShootAt(target);
    }

}