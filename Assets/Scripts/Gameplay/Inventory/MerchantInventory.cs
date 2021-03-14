using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Merchant")]
public class MerchantInventory : Inventory
{
    public List<InventoryStorage> goods = new List<InventoryStorage>();
    public List<Item> items = new List<Item>();
    [Range(0, 100)]
    public float priceModifier;
    override public List<InventoryStorage> m_content { get { return goods; } }
}