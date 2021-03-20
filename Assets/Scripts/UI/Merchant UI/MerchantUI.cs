using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MerchantUI : MonoBehaviour
{
    public Merchant merchant;
    public Player player;

    [SerializeField] protected RectTransform itemPanelContentMerchant = null;
    [SerializeField] protected RectTransform itemPanelContentPlayer = null;
    [SerializeField] protected TMPro.TMP_Text moneyPlayer = null;
    [SerializeField] protected TMPro.TMP_Text boatLevel = null;
    [SerializeField] protected TMPro.TMP_Text costAmeliorationMoney = null;
    [SerializeField] protected TMPro.TMP_Text costAmeliorationWood = null;
    [SerializeField] protected TMPro.TMP_Text costAmeliorationScraps = null;


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
        cardUI.MerchantOnSellItemEvent.AddListener(this.SellItem);
    }

    public void CreateCardPlayer(InventoryStorage item, InventoryStorage itemPlayer, RectTransform container)
    {
        PlayerForSaleItemsUI cardUI = GameObject.Instantiate(itemCardPrefabPlayer, container).GetComponent<PlayerForSaleItemsUI>();
        cardUI.InventoryItem = itemPlayer;
        cardUI.UpdateUI(item);
        cardUI.MerchantOnSellItemEvent.AddListener(this.BuyItem);
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
        SetLevelBoat(player.LevelBoat);
        foreach (InventoryStorage item in merchantInventory.items)
            CreateCard(item, itemPanelContentMerchant);

        var query = from itemMerchant in merchantInventory.items
                    join itemPlayer in playerInventory.items on itemMerchant.item.Name equals itemPlayer.item.Name
                    select new { PlayerItem = itemPlayer, MerchantItem = itemMerchant };

        foreach (var item in query)
            CreateCardPlayer(item.MerchantItem, item.PlayerItem, itemPanelContentPlayer);        
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

    public void SetLevelBoat(int level)
    {
        boatLevel.SetText(level.ToString());
        SetCostNextAmeliorationBoat(level);
    }

    public void SetCostNextAmeliorationBoat(int level)
    {
        costAmeliorationMoney.SetText("Money : " + (level * Merchant.goldModifier).ToString());
        costAmeliorationWood.SetText("Wood : " + (level * Merchant.woodModifier).ToString());
        costAmeliorationScraps.SetText("Scraps : " + (level * Merchant.scrapsModifier).ToString());
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
