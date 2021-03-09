using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacableInventoryUI : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] public InventoryStorageEvent onSelected = new InventoryStorageEvent();
    [Header("References")]
    [SerializeField] protected PlayerInventory playerInventory = null;
    [SerializeField] protected RectTransform placableObjectBarContent = null;
    [SerializeField] protected GameObject placableObjectButtonPrefab = null;
    [Header("Runtime")]
    [SerializeField] List<SelectableObjectButtonUI> buttons = new List<SelectableObjectButtonUI>();
    
    public void SelectPlacableItem(InventoryStorage stored)
    {
        onSelected.Invoke(stored);
    }

    public bool PlaceItem(InventoryStorage stored)
    {
        playerInventory.Remove(stored.item);
        InventoryStorage newStored = playerInventory.GetStoredItem(stored.item);
        if (newStored == null) {
            RemoveButton(stored);
            return true;
        }
        UpdateButton(newStored);
        return false;
    }

    public void RemoveItem(Item item)
    {
        playerInventory.Add(item);
        InventoryStorage newStored = playerInventory.GetStoredItem(item);
        if (newStored.count == 1) {
            CreateButton(newStored);
        } else {
            UpdateButton(newStored);
        }
    }

    public void UpdateButton(InventoryStorage stored)
    {
        SelectableObjectButtonUI target = buttons.Find((button) => button.Stored == stored);

        target.Stored = stored;
    }

    public void RemoveButton(InventoryStorage stored)
    {
        SelectableObjectButtonUI target = buttons.Find((button) => button.Stored == stored);

        buttons.Remove(target);
        GameObject.Destroy(target.gameObject);
    }

    public void CreateButton(InventoryStorage stored)
    {
        SelectableObjectButtonUI buttonUI = GameObject.Instantiate(placableObjectButtonPrefab, placableObjectBarContent).GetComponent<SelectableObjectButtonUI>(); // TODO: easy but dirty. Maybe add them along when adding to inventory

        buttonUI.Stored = stored;
        buttonUI.onSelected.AddListener(this.SelectPlacableItem);
        buttons.Add(buttonUI);
    }

    public void BuildUI()
    {
        foreach (InventoryStorage stored in playerInventory.items)
        {
            if (stored.item.placable == false) {
                continue;
            }
            CreateButton(stored);
        }
    }

    public void ClearUI()
    {
        buttons.Clear();
        placableObjectBarContent.ClearChilds(); // TODO: easy but dirty. Maybe remove them along when adding to inventory
    }

    private void OnEnable() {
        BuildUI();
    }

    private void OnDisable() {
        ClearUI();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
