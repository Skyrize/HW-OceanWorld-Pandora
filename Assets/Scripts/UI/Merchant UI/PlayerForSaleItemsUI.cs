using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerForSaleItemsUI : MerchantItemUI
{
    public InventoryStorage InventoryItem { get; set; }

    public override void UpdateUI(InventoryStorage item, MerchantUI merchant)
    {
        base.UpdateUI(item, merchant);
        quantityText.text = "Quantity :" + this.InventoryItem.count;
    }
}
