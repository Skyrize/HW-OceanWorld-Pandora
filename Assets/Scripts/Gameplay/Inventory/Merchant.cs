using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Merchant : MonoBehaviour
{
    [Header("References")]
    [SerializeField] public Player player;
    [SerializeField] public MerchantInventory inventoryMerchant;

    public PlayerInventory InventoryPlayer { get => player.inventory; }
    public MerchantInventory InventoryMerchant { get => inventoryMerchant; }

    public MerchantUI merchantUI;
    private static bool loaded;
    
    private void Awake() {
        inventoryMerchant = ClonableSO.Clone<MerchantInventory>(inventoryMerchant);
    }

    public void EnterInMerchant() 
    {  
        if (!loaded)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Merchant", LoadSceneMode.Additive);
            loaded = true;
        }
    }

    public void ExitMerchant()
    {
        if (loaded)
        {
            UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("Merchant");
            loaded = false;
        }
            
    }

    public void ClientBuyItem(Item item, float price)
    {
        InventoryStorage storage = inventoryMerchant.GetStoredItem(item);
        if (player.inventory.Money >= price && storage != null)
        {
            player.inventory.Add(item);
            inventoryMerchant.Remove(item);

            player.inventory.Money -= price;
        }
        merchantUI.BuildUI();
    }

    public void ClientSellItem(Item item, float price)
    {
        InventoryStorage storage = player.inventory.GetStoredItem(item);
        if(storage != null && storage.count > 0)
        {
            player.inventory.Remove(item);
            inventoryMerchant.Add(item);
            player.inventory.Money += price;
        }

        merchantUI.BuildUI();
    }
}
