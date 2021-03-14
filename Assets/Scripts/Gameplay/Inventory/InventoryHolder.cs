using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHolder : AbstractInventoryHolder
{
    [Header("References")]
    [SerializeField] protected BasicInventory inventoryAsset = null;
    
    protected BasicInventory inventory = null;
    override public BasicInventory Inventory {
        get {
            if (!inventory)
                inventory = ClonableSO.Clone<BasicInventory>(inventoryAsset);
            return inventory;
        }
    }
}
