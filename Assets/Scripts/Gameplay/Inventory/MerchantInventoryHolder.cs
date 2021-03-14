using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantInventoryHolder : AbstractInventoryHolder
{
    [Header("References")]
    [SerializeField] protected MerchantInventory inventoryAsset = null;
    
    protected MerchantInventory inventory = null;
    override public BasicInventory Inventory {
        get {
            if (!inventory)
                inventory = ClonableSO.Clone<MerchantInventory>(inventoryAsset);
            return null;
        }
    }
}
