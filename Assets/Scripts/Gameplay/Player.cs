using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("References")]
    [SerializeField] public Weaponry weaponry = null;
    [SerializeField] public Post controlPost = null;
    [SerializeField] public RepairStation repairStation = null;
    [SerializeField] protected PlayerInventory inventoryAsset = null;
    [Header("Runtime")]
    [SerializeField] public PlayerInventory inventory = null;

    public void Shoot(float input)
    {
        if (input == 0 || !isActiveAndEnabled)
            return;
            Debug.Log("shoot");
        weaponry.ShootAt(Utils.MousePositionOcean);
    }

    private void Awake() {
        inventory = ClonableSO.Clone<PlayerInventory>(inventoryAsset);
        if (!weaponry) {
            weaponry = GetComponent<Weaponry>();
        }
        if (weaponry) {
            InputManager.Instance.AddAxisEvent("Fire1", Shoot);
        }
    }

    /// <summary>
    /// Adds an object to the inventory
    /// </summary>
    /// <param name="obj">The item to add</param>
    /// <exception cref="NullReferenceException">The item is not a item</exception>
    public void CollectItem(GameObject obj)
    {
        Item item = obj.GetComponent<ItemObject>().Item;
        inventory.Add(item);
        Destroy(obj);
    }

    public void RemoveItem(GameObject obj)
    {
        Item item = obj.GetComponent<ItemObject>().Item;
        inventory.Remove(item);
    }
    public void RemoveItem(Item item, uint count = 1)
    {
        inventory.Remove(item, count);
    }

}
