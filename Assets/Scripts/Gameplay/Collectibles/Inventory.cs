using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private readonly Dictionary<string, uint> m_content = new Dictionary<string, uint>();

    [HideInInspector] public float TotalWeight;

    public void Collect(GameObject obj)
    {
        var collectable = obj.GetComponent<Collectable>();

        if (!collectable)
            throw new NullReferenceException("Collider is not collectable");

        if (!m_content.ContainsKey(collectable.Name))
            m_content.Add(collectable.Name, 0);

        m_content[collectable.Name] += collectable.Count;
        TotalWeight += collectable.Weight;

        Destroy(obj);
        DebugPrintInventory();
    }

    public void Remove(string objectName, uint count = 1)
    {
        if (!m_content.ContainsKey(objectName))
            throw new KeyNotFoundException("Object is not in inventory");

        m_content[objectName] -= count;

        if (m_content[objectName] == 0)
            m_content.Remove(objectName);
    }

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