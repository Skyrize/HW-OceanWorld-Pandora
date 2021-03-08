using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Inventory inventory;

    /// <summary>
    /// Adds an object to the inventory
    /// </summary>
    /// <param name="obj">The item to add</param>
    /// <exception cref="NullReferenceException">The item is not a collectible</exception>
    public void CollectItem(GameObject obj)
    {
        Collectible collectible = obj.GetComponent<Collectible>();
        if (collectible == null)
            throw new NullReferenceException("Collider is not collectable");
        inventory.AddItemToInventory(collectible);
        Destroy(obj);
    }

    public void RemoveItem(GameObject obj)
    {
        Collectible collectible = obj.GetComponent<Collectible>();
        if (collectible == null)
            throw new NullReferenceException("Collider is not throwable");
        inventory.RemoveItemFromInventory(collectible);
    }
}
