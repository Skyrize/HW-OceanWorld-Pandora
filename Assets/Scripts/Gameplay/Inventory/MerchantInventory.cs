using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MerchantStorage : InventoryStorage
{
    public float price;
}

[CreateAssetMenu(menuName = "Inventory/Merchant")]
public class MerchantInventory : Inventory<MerchantStorage>
{
    public List<MerchantStorage> _content = new List<MerchantStorage>();
    override public List<MerchantStorage> m_content { get { return _content; } }
}