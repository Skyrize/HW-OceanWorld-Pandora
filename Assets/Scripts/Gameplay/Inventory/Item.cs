using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

[System.Serializable]
public class ItemEvent : UnityEvent<Item>
{
}

[CreateAssetMenu(menuName = "Item/Item")]
public class Item : ClonableSO
{
    [Header("Settings")]
    [Min(0f)] public float weight = 1.0f;

    [Header("References")]
    public GameObject prefab;
    public Sprite icon;

    public string Name = "default";

    public bool Equals(Item other)
    {
        return other != null && other.Name == this.Name;
    }

    public override int GetHashCode()
    {
        if (Name == null) return 0;
        return Name.GetHashCode();
    }
    
    private void OnValidate() {
        if (prefab) {
            ItemHolder holder = prefab.GetComponent<ItemHolder>();

            if (!holder) {
                holder = prefab.AddComponent<ItemHolder>();
            }
            if (!holder.item)
                holder.item = this;

            Name = new UnityEditor.SerializedObject(this).FindProperty("m_Name").stringValue;
        }
    }

    override protected ClonableSO Clone()
    {
        var clone = base.Clone() as Item;

        clone.Name = this.Name;
        return clone;
    }
    

}
