using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void MerchantSellItemEventHandler(InventoryMerchantEventArgs e);

public class MerchantForSaleItemsUI : MerchantItemUI
{
    private event MerchantSellItemEventHandler MerchantSellItemEvent;

    public void Subscription(MerchantSellItemEventHandler method)
    {
        this.MerchantSellItemEvent += method;
    }

    public override void UpdateUI(MerchantStorage item)
    {
        base.UpdateUI(item);
        quantityText.text = "Quantity :" + this.item.count;
    }

    public void OnClick()
    {
        MerchantSellItemEvent?.Invoke(new InventoryMerchantEventArgs(this.item));
    }
}
