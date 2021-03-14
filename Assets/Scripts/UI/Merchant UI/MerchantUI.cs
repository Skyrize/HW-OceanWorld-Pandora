using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MerchantUI : MonoBehaviour
{
    public Merchant merchant;

    [SerializeField] protected RectTransform itemPanelContentMerchant = null;
    [SerializeField] protected RectTransform itemPanelContentPlayer = null;

    [SerializeField] protected GameObject itemCardPrefab = null;
    [SerializeField] protected GameObject itemCardPrefabPlayer = null;

    private MerchantInventory merchantInventory;
    private PlayerInventory playerInventory;

    public void GetInventory()
    {
        merchantInventory = merchant.InventoryMerchant;
        playerInventory = merchant.InventoryPlayer;
    }

    private void Awake()
    {
        GetInventory();
    }

    public void CreateCard(MerchantStorage item, RectTransform container)
    {
        MerchantForSaleItemsUI cardUI = GameObject.Instantiate(itemCardPrefab, container).GetComponent<MerchantForSaleItemsUI>();
        cardUI.UpdateUI(item);
        cardUI.Subscription(this.SellItem);
    }

    public void CreateCardPlayer(MerchantStorage item, InventoryStorage itemPlayer, RectTransform container)
    {
        PlayerForSaleItemsUI cardUI = GameObject.Instantiate(itemCardPrefabPlayer, container).GetComponent<PlayerForSaleItemsUI>();
        cardUI.InventoryItem = itemPlayer;
        cardUI.UpdateUI(item);
        cardUI.Subscription(this.BuyItem);
    }


    public void ClearUI()
    {
        itemPanelContentMerchant.ClearChilds();
        itemPanelContentPlayer.ClearChilds(); 
    }

    public void BuildUI()
    {
        ClearUI();
        foreach (MerchantStorage item in merchantInventory.m_content)
            CreateCard(item, itemPanelContentMerchant);

        var query = from itemMerchant in merchantInventory.m_content
                    join itemPlayer in playerInventory.m_content on itemMerchant.item.Name equals itemPlayer.item.Name
                    select new { PlayerItem = itemPlayer, MerchantItem = itemMerchant };

        foreach (var item in query)
            CreateCardPlayer(item.MerchantItem, item.PlayerItem, itemPanelContentPlayer);        
    }

    public void SellItem(InventoryMerchantEventArgs e)
    {
        merchant.ClientBuyItem(e.merchantStorage.item, e.merchantStorage.price) ;
    }

    public void BuyItem(InventoryMerchantEventArgs e)
    {
        merchant.ClientSellItem(e.merchantStorage.item, e.merchantStorage.price);
    }

    private void OnEnable()
    {
        BuildUI();
    }

    private void OnDisable()
    {
        ClearUI();
    }
}
