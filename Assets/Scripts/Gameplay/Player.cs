using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Inventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Adds an object to the inventory
    /// </summary>
    /// <param name="obj">The item to add</param>
    /// <exception cref="NullReferenceException">The item is not a collectible</exception>
    public void CollectItem(GameObject obj)
    {
        Collectable collectable = obj.GetComponent<Collectable>();
        if (collectable == null)
            throw new NullReferenceException("Collider is not collectable");
        inventory.AddItemToInventory(collectable);
        Destroy(obj);
    }

    public void RemoveItem(GameObject obj)
    {
        Collectable collectable = obj.GetComponent<Collectable>();
        if (collectable == null)
            throw new NullReferenceException("Collider is not throwable");
        inventory.RemoveItemFromInventory(collectable);
    }
}
