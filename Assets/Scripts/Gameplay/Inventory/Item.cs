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
    [Min(0f)]
    [SerializeField] protected float weight = 1.0f;
    public float Weight => weight;

    [SerializeField] protected float price = 1.0f;
    public float Price => price;

    [Header("References")]
    [SerializeField] protected GameObject prefab;
    public GameObject Prefab => prefab;
    [SerializeField] protected Sprite icon;
    public Sprite Icon => icon;

    protected string _name = "default";
    public string Name => _name;

    public bool Equals(Item other)
    {
        return other != null && other.Name == this.Name;
    }

    public override int GetHashCode()
    {
        if (Name == null) return 0;
        return Name.GetHashCode();
    }
    
    protected void OnValidate() {
        if (prefab) {
            ItemObject holder = prefab.GetComponent<ItemObject>();

            if (!holder) {
                holder = prefab.AddComponent<ItemObject>();
            }
            if (!holder.Item) {
                holder.DONOTCALL_SetItem(this);
            }

        }
        _name = name;
    }

    override protected ClonableSO Clone()
    {
        var clone = base.Clone() as Item;

        clone._name = this.Name;
        return clone;
    }
    

}
