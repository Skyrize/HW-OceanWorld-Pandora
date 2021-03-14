using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantForSaleItemsUI : MerchantItemUI
{ 
    public override void UpdateUI(InventoryStorage item)
    {
        base.UpdateUI(item);
        quantityText.text = "Quantity :" + this.item.count;
    }
}
