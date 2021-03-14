using System.Net.NetworkInformation;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InventoryStorageEvent : UnityEvent<InventoryStorage>
{
}

// TODO : check if Instantiate does deep copy this struct
[Serializable]
public class InventoryStorage {
    public Item item = null;
    public uint count = 0;

    public InventoryStorage(Item item = null, uint count = 0)
    {
        this.item = item;
        this.count = count;
    }
    public InventoryStorage()
    {
    }
}

public abstract class Inventory : ClonableSO
{
    abstract public List<InventoryStorage> m_content { get; }
    public float Money { get; set; }

    [HideInInspector] public float TotalWeight;

    /// <summary>
    /// Removes an object from the inventory
    /// </summary>
    /// <param name="objectName">The to remove</param>
    /// <param name="count">How many items to remove, defaults to 1</param>
    /// <exception cref="KeyNotFoundException">The object was not found in the inventory</exception>
    /// <exception cref="ArgumentException">Count was greater than the remaining count of the item</exception>
    public void Remove(Item item, uint count = 1)
    {
        InventoryStorage storage = GetStoredItem(item);
        if (storage == null)
            throw new KeyNotFoundException("Object is not in inventory");

        storage.count -= count;
        if (storage.count < 0) {
            storage.count = 0;
            throw new ArgumentException("Trying to remove more item than remaining count");
        }

        if (storage.count == 0) {
            m_content.Remove(storage);
        }
    }

    /// <summary>
    /// Add a collectable to the inventory
    /// </summary>
    /// <param name="item"></param>
    public void Add(Item item, uint count = 1)
    {
        InventoryStorage storage = GetStoredItem(item);
        if (storage == null) {
            InventoryStorage newItem = new InventoryStorage(item, count);
            
            m_content.Add(newItem);
            return;
        }

        storage.count += count;
        
        TotalWeight += item.Weight;
        
        if(ResourcePickupUI.Instance) {
            ResourcePickupUI.Instance.PopMessage($"+{count} {item.name}");
        }
    }

    public InventoryStorage GetStoredItem(Item item)
    {
        return m_content.Find((stored) => { return item.Equals(stored.item); });;
    }

    override protected ClonableSO Clone()
    {
        Inventory clone = base.Clone() as Inventory;

        for (int i = 0; i != clone.m_content.Count; i++) {
            clone.m_content[i].item = ClonableSO.Clone<Item>(clone.m_content[i].item);
        }
        return clone;
    }

    public List<InventoryStorage> GetItemsOfType(Type type)
    {
        List<InventoryStorage> items = new List<InventoryStorage>();

        foreach (InventoryStorage stored in m_content)
        {
            if (stored.item.GetType() == type) {
                items.Add(stored);
            }
        }
        return items;
    }

//     /// <summary>
//     /// Prints the inventory content, only available in editor mode
//     /// </summary>
//     private void DebugPrintInventory()
//     {
// #if UNITY_EDITOR
//         Debug.Log("Inventory:");
//         foreach (var item in m_content)
//         {
//             Debug.Log(item.Key + " " + item.Value);
//         }
// #endif
//     }
}