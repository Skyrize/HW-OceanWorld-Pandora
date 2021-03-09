using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SelectableObjectButtonUI : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] public InventoryStorageEvent onSelected = new InventoryStorageEvent();
    [Header("References")]
    [SerializeField] protected InventoryStorage stored = null;
    [SerializeField] public InventoryStorage Stored {
        get {
            return stored;
        }
        set {
            stored = value;
            if (stored != null) {
                UpdateUI();
            }
        }
    }
    [SerializeField] protected Image iconPlacement = null;
    [SerializeField] protected TMPro.TMP_Text countText = null;

    public void Select()
    {
        onSelected.Invoke(stored);
    }

    private void UpdateUI()
    {
        iconPlacement.sprite = stored.item.icon;
        countText.text = "x" + stored.count.ToString();
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
