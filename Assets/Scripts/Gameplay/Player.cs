using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerInventory inventory;

    /// <summary>
    /// Adds an object to the inventory
    /// </summary>
    /// <param name="obj">The item to add</param>
    /// <exception cref="NullReferenceException">The item is not a item</exception>
    public void CollectItem(GameObject obj)
    {
        Item item = obj.GetComponent<Item>();
        if (item == null)
            throw new NullReferenceException("Collider is not collectable");
        inventory.AddItemToInventory(item);
        Destroy(obj);
    }

    public void RemoveItem(GameObject obj)
    {
        Item item = obj.GetComponent<Item>();
        if (item == null)
            throw new NullReferenceException("Collider is not throwable");
        inventory.RemoveItemFromInventory(item);
    }
}
