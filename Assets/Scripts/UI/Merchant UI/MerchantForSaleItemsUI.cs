public class MerchantForSaleItemsUI : MerchantItemUI
{
    public override void UpdateUI(InventoryStorage item, MerchantUI merchant)
    {
        base.UpdateUI(item, merchant);
        quantityText.text = "Quantity :" + this.item.count;
    }
}
