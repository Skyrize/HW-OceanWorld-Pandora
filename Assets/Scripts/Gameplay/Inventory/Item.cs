﻿using System.Collections;
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

    [Header("References")]
    public GameObject prefab;
    public Sprite icon;
    
    private void OnValidate() {
        if (prefab) {
            ItemHolder holder = prefab.GetComponent<ItemHolder>();

            if (!holder) {
                holder = prefab.AddComponent<ItemHolder>();
            }
            if (!holder.item)
                holder.item = this;
        }
    }
}