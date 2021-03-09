using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Player")]
public class PlayerInventory : Inventory<InventoryStorage>
{
    public List<InventoryStorage> items = new List<InventoryStorage>();
    override public List<InventoryStorage> m_content { get { return items; } }

}
