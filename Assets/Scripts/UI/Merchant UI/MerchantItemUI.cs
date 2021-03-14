using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class InventoryMerchantEventArgs : EventArgs
{
    public MerchantStorage merchantStorage;
    public InventoryStorage inventoryStorage;

    public InventoryMerchantEventArgs(MerchantStorage item)
    {
        merchantStorage = item;
    }

    public InventoryMerchantEventArgs(MerchantStorage itemMerchant, InventoryStorage itemPlayer)
    {
        merchantStorage = itemMerchant;
        inventoryStorage = itemPlayer;
    }
}

public class MerchantItemUI : MonoBehaviour
{
    [Header("References")]
    //[SerializeField] protected SelectCrewMemberButtonUI button = null;
    [SerializeField] protected TMPro.TMP_Text nameText = null;
    [SerializeField] protected TMPro.TMP_Text costText = null;
    [SerializeField] protected TMPro.TMP_Text quantityText = null;
    [SerializeField] protected Image image = null;
    [Header("Runtime")]
    [SerializeField] protected MerchantStorage item = null;


    public MerchantStorage Item { get { return item; } }

    private void Start()
    {
        //button.onSelect.AddListener(Select);
        //button.onUnselect.AddListener(Unselect);
    }

    public virtual void UpdateUI(MerchantStorage item)
    {
        this.item = item;
        //button.UpdateUI(this.crewMember);
        nameText.text = "Name : " + this.item.item.Name;
        costText.text = "Cost : " + this.item.price;
        image.sprite = this.item.item.Icon;
    }
}
