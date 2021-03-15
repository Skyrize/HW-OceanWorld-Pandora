using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerForSaleItemsUI : MerchantItemUI
{
    public InventoryStorage InventoryItem { get; set; }

    public override void UpdateUI(InventoryStorage item)
    {
        base.UpdateUI(item);
        quantityText.text = "Quantity :" + this.InventoryItem.count;
    }
}
