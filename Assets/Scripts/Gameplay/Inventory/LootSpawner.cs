using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class SpawnItem
{
    public Item item;
    public uint CountMin;
    public uint CountMax;
}

public class LootSpawner : MonoBehaviour
{
    public List<SpawnItem> SpawnItems;
    public float SpreadForceMin = 50f;
    public float SpreadForceMax = 100f;
    [Range(0, 360)] public float SpreadAngle = 360f;

    public void SpawnLoot()
    {
        foreach (var item in SpawnItems)
        {
            if (!item.item)
            {
                Debug.LogWarning("Empty item in Resource Spawner");
                continue;
            }

            var count = Random.Range(item.CountMin, item.CountMax);

            for (var i = 0; i < count; i++)
            {
                var rotationY = Random.Range(0, 360);
                var rotation = Quaternion.Euler(new Vector3(0, rotationY, 0));

                var obj = Instantiate(item.item.Prefab, transform.position, Quaternion.identity);
                var body = obj.GetComponent<Rigidbody>();

                obj.transform.rotation = rotation;

                if (body)
                {
                    var angle = Mathf.Deg2Rad * Random.Range(0, SpreadAngle);
                    var spreadForce = Random.Range(SpreadForceMin, SpreadForceMax);
                    var force = new Vector3 {x = Mathf.Cos(angle) * spreadForce, z = Mathf.Sin(angle) * spreadForce};

                    body.AddForce(force);
                }
            }
        }
    }
}