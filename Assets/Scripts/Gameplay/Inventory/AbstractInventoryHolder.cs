using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractInventoryHolder : MonoBehaviour
{
    abstract public BasicInventory Inventory { get; }

    public void CollectItem(GameObject obj)
    {
        Item item = obj.GetComponent<ItemObject>().Item;
        Inventory.Add(item);
        Destroy(obj);
    }

    public void RemoveItem(GameObject obj)
    {
        Item item = obj.GetComponent<ItemObject>().Item;
        Inventory.Remove(item);
    }
    public void RemoveItem(Item item)
    {
        Inventory.Remove(item);
    }
}
