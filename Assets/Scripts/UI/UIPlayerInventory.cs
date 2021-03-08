using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerInventory : MonoBehaviour
{
    public Inventory inventoryPlayer;
    public GameObject inventoryItem;

    void Start()
    {
        gameObject.transform.Find("Money/MoneyDisplay").GetComponent<Text>().text = inventoryPlayer.Money.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Find("Money/MoneyDisplay").GetComponent<Text>().text = inventoryPlayer.Money.ToString();
    }
}
