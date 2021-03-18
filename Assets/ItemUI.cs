using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected InventoryStorage stored = null;
    [SerializeField] protected Image iconPlacement = null;
    [SerializeField] protected TMPro.TMP_Text countText = null;

    public void UpdateUI(InventoryStorage stored)
    {
        this.stored = stored;
        iconPlacement.sprite = this.stored.item.Icon;
        countText.text = "x" + this.stored.count.ToString();
    }
}
