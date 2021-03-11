using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    public Item item = null;

    private void OnValidate() {
        if (item && !item.prefab) {
            item.prefab = this.gameObject;
        }
    }
}
