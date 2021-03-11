using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("References")]
    [SerializeField] public Weaponry weaponry = null;
    [SerializeField] protected PlayerInventory inventoryAsset = null;
    [Header("Runtime")]
    [SerializeField] public PlayerInventory inventory = null;

    public void Shoot(float input)
    {
        if (input == 0 || !isActiveAndEnabled)
            return;
        weaponry.ShootAt(Utils.MousePosition);
    }

    private void Awake() {
        inventory = ClonableSO.Clone<PlayerInventory>(inventoryAsset);
        if (!weaponry)
            weaponry = GetComponent<Weaponry>();
        InputManager.Instance.AddAxisEvent("Fire1", Shoot);
    }

    /// <summary>
    /// Adds an object to the inventory
    /// </summary>
    /// <param name="obj">The item to add</param>
    /// <exception cref="NullReferenceException">The item is not a item</exception>
    public void CollectItem(GameObject obj)
    {
        Item item = obj.GetComponent<ItemObject>().item;
        inventory.Add(item);
        Destroy(obj);
    }

    public void RemoveItem(GameObject obj)
    {
        Item item = obj.GetComponent<ItemObject>().item;
        inventory.Remove(item);
    }

}
