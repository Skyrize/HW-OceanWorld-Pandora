using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private GameObject projectileContainer;
    public GameObject projectilePrefab;

    public Vector3 weaponOffset = new Vector3(0, 1.25f, .2f);

    public float shotDelay = 1;
    private float lastShot;

    private void Start()
    {
        lastShot = shotDelay;
        projectileContainer = GameObject.FindGameObjectWithTag(Utils.Tags.PROJECTILE_CONTAINER);
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
        var projectileObject = Instantiate(projectilePrefab, 
            transform.position, 
            transform.rotation, 
            projectileContainer.transform);
        var projectileScript = projectileObject.GetComponent<Projectile>();

        projectileScript.Origin = transform.position
            + transform.TransformDirection(weaponOffset); 
        projectileScript.Target = target;

        projectileObject.SetActive(true);
    }

    public bool CanShoot => lastShot >= shotDelay;
}