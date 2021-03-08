using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class ItemEvent : UnityEvent<TMP_InventoryItem>
{
}

public class SelectableObjectButtonUI : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] public ItemEvent onSelected = new ItemEvent();
    [Header("References")]
    [SerializeField] public TMP_InventoryItem item = null;
    [SerializeField] protected Image iconPlacement = null;

    public void Select()
    {
        onSelected.Invoke(item);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!iconPlacement)
            iconPlacement = GetComponentInChildren<Image>();
        iconPlacement.sprite = item.icon;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
