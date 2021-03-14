using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryHolder : AbstractInventoryHolder
{
    [Header("References")]
    [SerializeField] protected PlayerInventory inventoryAsset = null;
    
    protected PlayerInventory inventory = null;
    override public BasicInventory Inventory {
        get {
            if (!inventory)
                inventory = ClonableSO.Clone<PlayerInventory>(inventoryAsset);
            return inventory;
        }
    }
}
