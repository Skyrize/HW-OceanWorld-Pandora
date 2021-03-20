using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

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

    public override string ToString()
    {
        return $"{item.ToString()}]x{count}";
    }
}

[CreateAssetMenu(menuName = "Inventory/Basic")]
public class Inventory : ClonableSO
{
    public List<InventoryStorage> items = new List<InventoryStorage>(); //TODO : protected when MerchantUI doesn't use it directly anymore.
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
            items.Remove(storage);
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
            
            items.Add(newItem);
        }
        else
        {
            storage.count += count;
            TotalWeight += item.Weight;
        }
        
        if(ResourcePickupUI.Instance) {
            ResourcePickupUI.Instance.PopMessage($"+{count} {item.name}");
        }
    }

    public InventoryStorage GetStoredItem(Item item)
    {
        return items.Find((stored) => { return item.Equals(stored.item); });
    }

    public uint CountItem(string name)
    {
        return items.Find((stored) => { return name.Equals(stored.item.Name); }).count;
    }

    override protected ClonableSO Clone()
    {
        Inventory clone = base.Clone() as Inventory;

        for (int i = 0; i != clone.items.Count; i++) {
            clone.items[i].item = ClonableSO.Clone<Item>(clone.items[i].item);
        }
        return clone;
    }

    public List<InventoryStorage> GetItemsOfType(Type type)
    {
        List<InventoryStorage> result = new List<InventoryStorage>();

        foreach (InventoryStorage stored in items)
        {
            if (stored.item.GetType() == type) {
                result.Add(stored);
            }
        }
        return result;
    }

    override public string ToString()
    {
        string result = "";

        foreach (var store in items)
        {
            result += $"[{store.ToString()}]\n";
        }

        return result;
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