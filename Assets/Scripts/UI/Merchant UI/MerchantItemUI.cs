using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MerchantItemUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected TMPro.TMP_Text nameText = null;
    [SerializeField] protected TMPro.TMP_Text costText = null;
    [SerializeField] protected TMPro.TMP_Text quantityText = null;

    [SerializeField] protected Image image = null;
    [Header("Runtime")]
    [SerializeField] protected InventoryStorage item = null;
    public InventoryStorageEvent MerchantOnSellItemEvent = new InventoryStorageEvent();

    public InventoryStorage Item { get { return item; } }


    public void UpdateUI(InventoryStorage item)
    {
        this.item = item;
        nameText.text = "Name : " + this.item.item.Name;
        costText.text = "Cost : " + this.item.item.Price;
        quantityText.text = "Quantity :" + this.item.count;
        image.sprite = this.item.item.Icon;
    }

    public void OnClick()
    {
        MerchantOnSellItemEvent.Invoke(this.item);
    }
}
