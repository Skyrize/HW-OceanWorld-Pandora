using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected PlayerInventory inventoryAsset = null;
    [Header("Runtime")]
    public PlayerInventory inventory = null;

    private void Awake() {
        inventory = ClonableSO.Clone<PlayerInventory>(inventoryAsset);
    }

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
        inventory.Add(item);
        Destroy(obj);
    }

    public void RemoveItem(GameObject obj)
    {
        Item item = obj.GetComponent<Item>();
        if (item == null)
            throw new NullReferenceException("Collider is not throwable");
        inventory.Remove(item);
    }
}
