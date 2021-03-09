using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MerchantManager : MonoBehaviour
{
    public PlayerInventory inventoryPlayer;
    public MerchantInventory merchantInventory;

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

    public void ClientBuyItem(Item item, float price, uint count)
    {
        if (inventoryPlayer.Money >= price)
        {
            inventoryPlayer.Add(item, count);
            inventoryPlayer.Money -= price * count;
        }
        //TODO : add to merchant inventory (NOT MANDATORY)
    }

    public void ClientSellItem(Item item, float price)
    {
        InventoryStorage storage = inventoryPlayer.GetStoredItem(item);
        if(storage != null && storage.count > 0)
        {
            inventoryPlayer.Remove(item);
            inventoryPlayer.Money += price;
        }
        //TODO : remove from merchant inventory (NOT MANDATORY)
    }
}
