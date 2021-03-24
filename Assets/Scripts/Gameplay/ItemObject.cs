using System;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] protected Item item = null;
    public Item Item => item;

    [NonSerialized] public bool PickedUp = false;

    public void DONOTCALL_SetItem(Item item)
    {
        this.item = item;
    }
}
