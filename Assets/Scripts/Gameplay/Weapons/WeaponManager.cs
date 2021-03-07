using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject projectileContainer;
    public GameObject projectilePrefab;

    public string TargetType { get; set; }

    public Vector3 weaponOffset = new Vector3(0, 1.25f, .2f);

    public float shotDelay = 1;
    private float lastShot;

    private void Start()
    {
        lastShot = shotDelay;
    }

    void Update()
    {
        if (!CanShoot)
            lastShot += Time.deltaTime;
    }

    public void ShootAt(Vector3 target)
    {
        if (!CanShoot)
            return;

        lastShot = 0;
        var projectileObject = Instantiate(projectilePrefab, projectileContainer.transform);
        var projectileScript = projectileObject.GetComponent<Projectile>();

        print(weaponOffset);
        print(transform.position);
        print(Utils.Copy(transform.position)
            + transform.TransformDirection(weaponOffset));

        projectileScript.Origin = transform.position
            + transform.TransformDirection(weaponOffset); 
        projectileScript.Target = target;
        projectileScript.TargetType = TargetType;

        projectileObject.SetActive(true);
    }

    public bool CanShoot => lastShot >= shotDelay;
}

public enum ShootType
{
    CANONBALL,
}