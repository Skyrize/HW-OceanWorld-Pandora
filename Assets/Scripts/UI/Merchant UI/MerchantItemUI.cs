using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SellingEvent : UnityEvent<InventoryStorage, float>
{
}

public class MerchantItemUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected TMPro.TMP_Text nameText = null;
    [SerializeField] protected TMPro.TMP_Text costText = null;
    [SerializeField] protected TMPro.TMP_Text quantityText = null;

    [SerializeField] protected Image image = null;
    [Header("Runtime")]
    [SerializeField] protected InventoryStorage item = null;
    public SellingEvent MerchantOnSellItemEvent = new SellingEvent();

    public InventoryStorage Item { get { return item; } }
    public float ItemPrice { get; set; }

    public void UpdateUI(InventoryStorage item)
    {
        this.item = item;
        nameText.text = "Name : " + this.item.item.Name;
        costText.text = "Cost : " + ItemPrice;
        quantityText.text = "Quantity :" + this.item.count;
        image.sprite = this.item.item.Icon;
    }

    public void OnClick()
    {
        MerchantOnSellItemEvent.Invoke(this.item, ItemPrice);
    }
}
