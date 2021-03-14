using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Merchant : MonoBehaviour
{
    [Header("References")]
    //public PlayerInventory inventoryPlayer { get => player.inventory; };
    [SerializeField] public Player player;
    [SerializeField] public MerchantInventory inventoryMerchant;

    public PlayerInventory InventoryPlayer { get => player.inventory; }
    public MerchantInventory InventoryMerchant { get => inventoryMerchant; }

    public MerchantUI merchantUI;

    private void Awake() {
        inventoryMerchant = ClonableSO.Clone<MerchantInventory>(inventoryMerchant);
    }

    public void EnterInMerchant(GameObject obj)
    {
        if(!UnityEngine.SceneManagement.SceneManager.GetSceneByName("Merchant").isLoaded)
            UnityEngine.SceneManagement.SceneManager.LoadScene("Merchant", LoadSceneMode.Additive);
    }

    public void ExitMerchant()
    {
        Debug.Log("exit");
        if (UnityEngine.SceneManagement.SceneManager.GetSceneByName("Merchant").isLoaded)
            UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("Merchant");
    }

    public void ClientBuyItem(Item item, float price, uint count=1)
    {
        if (player.inventory.Money >= price)
        {
            player.inventory.Add(item);
            inventoryMerchant.Remove(item);

            player.inventory.Money -= price * count;
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
