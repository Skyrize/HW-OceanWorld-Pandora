using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [Header("References")]
    [HideInInspector] protected PlayerInventory playerInventory = null;
    [SerializeField] protected RectTransform itemsBarContent = null;
    [SerializeField] protected GameObject itemUIPrefab = null;

    private void Awake() {
        var player = GameObject.FindObjectOfType<Player>();

        if (player) {
            this.playerInventory = player.inventory;
        } else {
            throw new MissingReferenceException("No Player in scene to hold an inventory. It is need to ensure you don't modify an asset while using inventory.");
        }
    }

    public void BuildUI()
    {
        foreach (InventoryStorage stored in playerInventory.items)
        {
            ItemUI itemUI = GameObject.Instantiate(itemUIPrefab, itemsBarContent).GetComponent<ItemUI>(); // TODO: easy but dirty. Maybe add them along when adding to inventory

            itemUI.UpdateUI(stored);
        }
    }

    public void ClearUI()
    {
        itemsBarContent.ClearChilds(); // TODO: easy but dirty. Maybe remove them along when adding to inventory
    }

    private void OnEnable() {
        BuildUI();
    }

    private void OnDisable() {
        ClearUI();
    }
}
