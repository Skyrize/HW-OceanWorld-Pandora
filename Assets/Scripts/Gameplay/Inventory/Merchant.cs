using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Merchant : MonoBehaviour
{
    [Header("References")]
    public Player player;
    [SerializeField] protected MerchantInventory inventoryAsset;
    [HideInInspector] public MerchantInventory inventory;

    private void Awake() {
        
        inventory = ClonableSO.Clone<MerchantInventory>(inventoryAsset);
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

    public void ClientBuyItem(Item item, float price, uint count)
    {
        if (player.inventory.Money >= price)
        {
            player.inventory.Add(item, count);
            player.inventory.Money -= price * count;
        }
        //TODO : add to merchant inventory (NOT MANDATORY)
    }

    public void ClientSellItem(Item item, float price)
    {
        InventoryStorage storage = player.inventory.GetStoredItem(item);
        if(storage != null && storage.count > 0)
        {
            player.inventory.Remove(item);
            player.inventory.Money += price;
        }
        //TODO : remove from merchant inventory (NOT MANDATORY)
    }
}
