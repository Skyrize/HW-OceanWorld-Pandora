using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class TMP_InventoryItem {
    public GameObject item;
    public Sprite icon;
}

public class CrewManager : MonoBehaviour
{
    [Header("References")]

    [SerializeField] protected PlacableInventoryUI placableInventoryUI = null;
    [SerializeField] protected ItemPlacer itemPlacer = null;
    [Header("Runtime")]
    [SerializeField] protected bool toggle = false;
    // Start is called before the first frame update
    protected EventBinder events;
    void Start()
    {
        events = GetComponent<EventBinder>();
        placableInventoryUI.onSelected.AddListener(SelectPlacableItem);
        Exit();
    }

    public void SelectPlacableItem(TMP_InventoryItem item)
    {
        itemPlacer.CurrentItem = item;
    }

    public void Enter()
    {
        events.CallEvent("Enter");
    }

    public void Exit()
    {
        events.CallEvent("Exit");
        itemPlacer.CurrentItem = null;
    }

    public void Toggle()
    {
        toggle = !toggle;
        if (toggle) {
            Enter();
        } else {
            Exit();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
