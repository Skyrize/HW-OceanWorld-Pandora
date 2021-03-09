using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    public Item item = null;

    private void Awake() {
        if (item) {
            item.Name = gameObject.name;
        }
    }
}
