using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] protected Item item = null;
    public Item Item => item;

    public void DONOTCALL_SetItem(Item item)
    {
        this.item = item;
    }
}
