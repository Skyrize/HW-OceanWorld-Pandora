using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Merchant : MonoBehaviour
{
    [Header("References")]
    [SerializeField] public PlayerInventory inventoryPlayer;
    [SerializeField] public MerchantInventory inventoryMerchant;

    public PlayerInventory InventoryPlayer { get => inventoryPlayer; }
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
        if (inventoryPlayer.Money >= price)
        {
            inventoryPlayer.Add(item, count);
            inventoryMerchant.Remove(item, count);

            inventoryPlayer.Money -= price * count;
        }
        merchantUI.BuildUI();
        //TODO : add to merchant inventory (NOT MANDATORY)
    }

    public void ClientSellItem(Item item, float price)
    {
        InventoryStorage storage = inventoryPlayer.GetStoredItem(item);
        if(storage != null && storage.count > 0)
        {
            inventoryPlayer.Remove(item);
            inventoryMerchant.Add(item);
            inventoryPlayer.Money += price;
        }

        merchantUI.BuildUI();
        //TODO : remove from merchant inventory (NOT MANDATORY)
    }
}
