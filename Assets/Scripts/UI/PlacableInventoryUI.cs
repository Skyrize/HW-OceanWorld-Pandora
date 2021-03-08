using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlacableInventoryUI : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] public ItemEvent onSelected = new ItemEvent();
    [Header("References")]
    [SerializeField] protected RectTransform placableObjectBarContent = null;
    [SerializeField] protected GameObject placableObjectButtonPrefab = null;
    [Header("TMP")]
    [SerializeField]
    TMP_InventoryItem[] items = new TMP_InventoryItem[3];
    
    public void SelectPlacableItem(TMP_InventoryItem item)
    {
        onSelected.Invoke(item);
    }

    private void OnEnable() {
        // TODO: link with real inventory
        foreach (var item in items)
        {
            GameObject button = GameObject.Instantiate(placableObjectButtonPrefab, placableObjectBarContent); // TODO: easy but dirty. Maybe add them along when adding to inventory
            SelectableObjectButtonUI buttonUI = button.GetComponent<SelectableObjectButtonUI>();

            buttonUI.item = item;
            buttonUI.onSelected.AddListener(this.SelectPlacableItem);
        }
    }

    private void OnDisable() {
        placableObjectBarContent.ClearChilds(); // TODO: easy but dirty. Maybe remove them along when adding to inventory
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
