using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/InventoryPlayer")]
public class Inventory : ScriptableObject
{
    public readonly Dictionary<Collectable, uint> m_content = new Dictionary<Collectable, uint>();
    public float Money { get; set; }

    [HideInInspector] public float TotalWeight;


    /// <summary>
    /// Removes an object from the inventory
    /// </summary>
    /// <param name="objectName">The to remove</param>
    /// <param name="count">How many items to remove, defaults to 1</param>
    /// <exception cref="KeyNotFoundException">The object was not found in the inventory</exception>
    public void RemoveItemFromInventory(Collectable item, uint count = 1)
    {
        if (!m_content.ContainsKey(item))
            throw new KeyNotFoundException("Object is not in inventory");

        m_content[item] -= count;

        if (m_content[item] == 0)
            m_content.Remove(item);
    }

    /// <summary>
    /// Add a collectable to the inventory
    /// </summary>
    /// <param name="item"></param>
    public void AddItemToInventory(Collectable item)
    {
        if (!m_content.ContainsKey(item))
            m_content.Add(item, 0);

        m_content[item] += item.Count;
        
        TotalWeight += item.Weight;
        
        if(ResourcePickupUI.Instance)
            ResourcePickupUI.Instance.PopMessage($"+{item.Count} {item.Name}");
    }

    /// <summary>
    /// Prints the inventory content, only available in editor mode
    /// </summary>
    private void DebugPrintInventory()
    {
#if UNITY_EDITOR
        Debug.Log("Inventory:");
        foreach (var item in m_content)
        {
            Debug.Log(item.Key + " " + item.Value);
        }
#endif
    }
}