using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MerchantUI : MonoBehaviour
{
    public Merchant merchant;

    [SerializeField] protected RectTransform itemPanelContentMerchant = null;
    [SerializeField] protected RectTransform itemPanelContentPlayer = null;
    [SerializeField] protected TMPro.TMP_Text moneyPlayer = null;

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

    public void CreateCard(InventoryStorage item, RectTransform container)
    {
        MerchantForSaleItemsUI cardUI = GameObject.Instantiate(itemCardPrefab, container).GetComponent<MerchantForSaleItemsUI>();
        cardUI.UpdateUI(item);
        cardUI.MerchantOnSellItemEvent.AddListener(SellItem);
    }

    public void CreateCardPlayer(InventoryStorage item, RectTransform container)
    {
        PlayerForSaleItemsUI cardUI = GameObject.Instantiate(itemCardPrefabPlayer, container).GetComponent<PlayerForSaleItemsUI>();
        cardUI.UpdateUI(item);
        cardUI.MerchantOnSellItemEvent.AddListener(BuyItem);
    }

    public void ClearUI()
    {
        itemPanelContentMerchant.ClearChilds();
        itemPanelContentPlayer.ClearChilds(); 
    }

    public void BuildUI()
    {
        ClearUI();
        SetMoney(playerInventory.Money);
        foreach (InventoryStorage item in merchantInventory.m_content)
            CreateCard(item, itemPanelContentMerchant);

        var query = from itemMerchant in merchantInventory.items
                    join itemPlayer in playerInventory.m_content on itemMerchant.Name equals itemPlayer.item.Name
                    select new { PlayerItem = itemPlayer };

        foreach (var item in query)
            CreateCardPlayer(item.PlayerItem, itemPanelContentPlayer);        
    }

    public void SellItem(InventoryStorage item)
    {
        merchant.ClientBuyItem(item.item, item.item.Price) ;
    }

    public void BuyItem(InventoryStorage item)
    {
        merchant.ClientSellItem(item.item, item.item.Price);
    }

    public void SetMoney(float money)
    {
        moneyPlayer.SetText(money.ToString("0.00"));
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
