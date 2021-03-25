using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ErrorEvent : UnityEvent<string>
{
}
[System.Serializable]
public class RepairEvent : UnityEvent<float, uint, Item>
{
}


public class RepairStation : Post
{
    [Header("Settings")]
    [SerializeField] protected float repairTime = 1f;
    [SerializeField] protected float repairAmount = 1f;
    [SerializeField] protected uint consumeQuantity = 0;
    [Header("Events")]
    [SerializeField] public RepairEvent onRepairDone = new RepairEvent();
    [SerializeField] public ErrorEvent onMissingResource = new ErrorEvent();
    [Header("References")]
    [SerializeField] protected InventoryHolder inventoryHolder = null;
    [SerializeField] protected Item consumedItem = null;
    public Item ConsumedItem => consumedItem;
    [Header("Runtime")]
    [SerializeField] protected bool repairing = false;
    public bool IsRepairing => repairing;
    [SerializeField] protected HealthComponent parentHealth = null;
    
    private WaitForSeconds repairTimer = null;

    public bool hasResource = false;
    override public void Awake() {
        base.Awake();
        repairTimer = new WaitForSeconds(repairTime);
        parentHealth = GetComponentInParent<HealthComponent>();
    }

    IEnumerator Repair()
    {
        repairing = true;
        yield return repairTimer;
        onRepairDone.Invoke(repairAmount, consumeQuantity, consumedItem);
        repairing = false;
        parentHealth.IncreaseHealth(repairAmount);
        inventoryHolder.RemoveItem(consumedItem, consumeQuantity);
    }

    override public void ClearEmployee()
    {
        base.ClearEmployee();
        if (repairing) {
            StopAllCoroutines();
            repairing = false;
        }
    }

    bool IsRepairNeeded()
    {
        return parentHealth.MaxHealth - parentHealth.Health >= repairAmount / 2.0f;
    }

    override protected void _Use()
    {
        if (repairing || !IsRepairNeeded())
            return;
        if (!inventoryHolder)
            inventoryHolder = GetComponentInParent<InventoryHolder>();
        var stored = inventoryHolder.inventory.GetStoredItem(consumedItem);
        uint count = stored != null ? stored.count : 0;
        if (count < consumeQuantity) {
            onMissingResource.Invoke($"Need {consumeQuantity} {consumedItem.Name} to repair and you only have {count} !");
            hasResource = false;
        } else {
            hasResource = true;
            StartCoroutine(Repair());
        }
    }
}
