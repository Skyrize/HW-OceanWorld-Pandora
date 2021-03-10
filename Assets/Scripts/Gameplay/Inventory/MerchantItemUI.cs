using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MerchantItemUI : MonoBehaviour
{
    public Merchant merchant;
    public MerchantStorage storedItem;

    // void Awake()
    // {
    //     gameObject.transform.Find("Price").GetComponent<Text>().text = price.ToString();
    // }

    public void OnSell()
    {
        merchant.ClientSellItem(storedItem.item, storedItem.price);
    }

    public void OnBuy()
    {
        merchant.ClientBuyItem(storedItem.item, storedItem.price, storedItem.count);
    }
}
