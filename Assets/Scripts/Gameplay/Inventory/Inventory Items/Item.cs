using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Item")]
public class Item : ScriptableObject
{
    [Header("Settings")]
    [Min(0f)] public float Weight = 1.0f;

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
