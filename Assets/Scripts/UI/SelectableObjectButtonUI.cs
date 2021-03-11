using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SelectableObjectButtonUI : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] public InventoryStorageEvent onSelect = new InventoryStorageEvent();
    [Header("References")]
    [SerializeField] protected InventoryStorage stored = null;
    [SerializeField] protected Image iconPlacement = null;
    [SerializeField] protected TMPro.TMP_Text countText = null;

    public InventoryStorage Stored { get {return stored; } }

    public void Select()
    {
        onSelect.Invoke(stored);
    }

    public void UpdateUI(InventoryStorage stored)
    {
        this.stored = stored;
        iconPlacement.sprite = this.stored.item.Icon;
        countText.text = "x" + this.stored.count.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!iconPlacement)
            iconPlacement = GetComponentInChildren<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
