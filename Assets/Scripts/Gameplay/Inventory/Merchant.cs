﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Merchant : MonoBehaviour
{
    [Header("References")]
    [SerializeField] public Player player;
    [SerializeField] public MerchantInventory inventoryMerchant;
    [SerializeField] public GameObject prefabUI;
    public PlayerInventory InventoryPlayer { get => player.inventory; }
    public MerchantInventory InventoryMerchant { get => inventoryMerchant; }

    public MerchantUI merchantUI;
    public PauseManager pauseManager;

    private static bool loadedScene;

    private void Awake()
    {  
        if (inventoryMerchant != null)
            inventoryMerchant = ClonableSO.Clone(inventoryMerchant);
        if (prefabUI.activeInHierarchy) prefabUI.SetActive(false);
    }

    public void EnterInMerchant() 
    {
        if (!loadedScene)
        {
            prefabUI.SetActive(true);
            //UnityEngine.SceneManagement.SceneManager.LoadScene("Merchant", LoadSceneMode.Additive);
            loadedScene = true;
            
            pauseManager.Pause();
        }
            
    }

    public void ExitMerchant()
    {
        if (loadedScene)
        {
            prefabUI.SetActive(false);
            //UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("Merchant");
            loadedScene = false;
            
            pauseManager.Unpause();
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
