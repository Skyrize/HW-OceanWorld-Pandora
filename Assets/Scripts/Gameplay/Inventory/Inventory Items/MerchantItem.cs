using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MerchantItem : MonoBehaviour
{
    public Collectible item;
    public MerchantManager merchant;
    public float price;

    void Awake()
    {
        gameObject.transform.Find("Price").GetComponent<Text>().text = price.ToString();
    }

    public void OnSell()
    {
        merchant.ClientSellItem(this.item, price);
    }

    public void OnBuy()
    {
        merchant.ClientBuyItem(this.item, price);
    }
}
