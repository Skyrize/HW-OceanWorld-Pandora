using UnityEngine;
using System.Linq;

public class MerchantUI : MonoBehaviour
{
    public Merchant merchant;

    [SerializeField] protected RectTransform itemPanelContentMerchant = null;
    [SerializeField] protected RectTransform itemPanelContentPlayer = null;
    [SerializeField] protected TMPro.TMP_Text moneyPlayer = null;

    [SerializeField] protected GameObject itemCardPrefab = null;

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
        MerchantItemUI cardUI = GameObject.Instantiate(itemCardPrefab, container).GetComponent<MerchantItemUI>();
        cardUI.ItemPrice = item.item.Price + item.item.Price * merchant.inventoryMerchant.priceModifier / 100;
        cardUI.UpdateUI(item);
        cardUI.MerchantOnSellItemEvent.AddListener(SellItem);
    }

    public void CreateCardPlayer(InventoryStorage item, RectTransform container)
    {
        MerchantItemUI cardUI = GameObject.Instantiate(itemCardPrefab, container).GetComponent<MerchantItemUI>();
        cardUI.ItemPrice = item.item.Price - item.item.Price * merchant.inventoryMerchant.priceModifier / 100;
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

    public void SellItem(InventoryStorage item, float price)
    {
        merchant.ClientBuyItem(item.item, price) ;
    }

    public void BuyItem(InventoryStorage item, float price)
    {
        merchant.ClientSellItem(item.item, price);
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
