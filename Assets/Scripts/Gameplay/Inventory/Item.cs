using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ItemEvent : UnityEvent<Item>
{
}

[CreateAssetMenu(menuName = "Item/Item")]
public class Item : ClonableSO
{
    [Header("Settings")]
    [Min(0f)] public float weight = 1.0f;
    [Min(0f)] public bool placable = false;

    [Header("References")]
    public GameObject prefab;
    public Sprite icon;

    public override bool Equals(object obj)
    {
        Item other = obj as Item;
        return other != null && other.name == this.name;
    }

    public override int GetHashCode()
    {
        if (name == null) return 0;
        return name.GetHashCode();
    }
}
