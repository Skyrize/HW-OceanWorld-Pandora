using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHolder : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected Inventory inventoryAsset = null;
    
    [Header("Runtime")]
    [SerializeField] protected Inventory _inventory = null;
    public Inventory inventory {
        get {
            if (!_inventory)
                _inventory = ClonableSO.Clone<Inventory>(inventoryAsset);
            return _inventory;
        }
    }

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
