using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private readonly Dictionary<string, uint> m_content = new Dictionary<string, uint>();

    [HideInInspector] public float TotalWeight;

    /// <summary>
    /// Adds an object to the inventory
    /// </summary>
    /// <param name="obj">The item to add</param>
    /// <exception cref="NullReferenceException">The item is not a collectible</exception>
    public void Collect(GameObject obj)
    {
        var collectable = obj.GetComponent<Collectable>();

        if (!collectable)
            throw new NullReferenceException("Collider is not collectable");

        if (!m_content.ContainsKey(collectable.Name))
            m_content.Add(collectable.Name, 0);

        m_content[collectable.Name] += collectable.Count;
        TotalWeight += collectable.Weight;
        
        ResourcePickupUI.Instance.PopMessage($"+{collectable.Count} {collectable.Name}");

        Destroy(obj);
    }

    /// <summary>
    /// Removes an object from the inventory
    /// </summary>
    /// <param name="objectName">The object name</param>
    /// <param name="count">How many items to remove, defaults to 1</param>
    /// <exception cref="KeyNotFoundException">The object was not found in the inventory</exception>
    public void Remove(string objectName, uint count = 1)
    {
        if (!m_content.ContainsKey(objectName))
            throw new KeyNotFoundException("Object is not in inventory");

        m_content[objectName] -= count;

        if (m_content[objectName] == 0)
            m_content.Remove(objectName);
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