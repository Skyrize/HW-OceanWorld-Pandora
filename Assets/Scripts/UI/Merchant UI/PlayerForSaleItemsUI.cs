using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public delegate void MerchantBuyItemEventHandler(InventoryMerchantEventArgs e);
public class PlayerForSaleItemsUI : MerchantItemUI
{
    public InventoryStorage InventoryItem { get; set; }
    private event MerchantBuyItemEventHandler MerchantBuyItemEvent;

    public void Subscription(MerchantBuyItemEventHandler method)
    {
        this.MerchantBuyItemEvent += method;
    }

    public override void UpdateUI(MerchantStorage item)
    {
        base.UpdateUI(item);
        quantityText.text = "Quantity :" + this.InventoryItem.count;
    }

    public void OnClick()
    {
        MerchantBuyItemEvent?.Invoke(new InventoryMerchantEventArgs(this.item, InventoryItem));
    }
}
