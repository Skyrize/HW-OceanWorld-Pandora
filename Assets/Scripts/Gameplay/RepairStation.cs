using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class RepairEvent : UnityEvent<float>
{
}

[System.Serializable]
public class ErrorEvent : UnityEvent<string>
{
}

public class RepairStation : Post
{
    [Header("Settings")]
    [SerializeField] protected float repairTime = 1f;
    [SerializeField] protected float repairAmount = 1f;
    [SerializeField] protected uint consumeQuantity = 0;
    [Header("Events")]
    [SerializeField] public RepairEvent onRepair = new RepairEvent();
    [SerializeField] public ErrorEvent onMissingResource = new ErrorEvent();
    [Header("References")]
    [SerializeField] protected InventoryHolder inventoryHolder = null;
    [SerializeField] protected Item consumedItem = null;
    [Header("Runtime")]
    [SerializeField] protected bool repairing = false;
    
    private WaitForSeconds repairTimer = null;

    private void Awake() {
        repairTimer = new WaitForSeconds(repairTime);
    }

    IEnumerator Repair()
    {
        repairing = true;
        yield return repairTimer;
        repairing = false;
        onRepair.Invoke(repairAmount);
        inventoryHolder.RemoveItem(consumedItem, consumeQuantity);
    }

    override protected void _Use()
    {
        if (repairing)
            return;
        if (!inventoryHolder)
            inventoryHolder = GetComponentInParent<InventoryHolder>();
        var stored = inventoryHolder.inventory.GetStoredItem(consumedItem);
        uint count = stored != null ? stored.count : 0;
        if (count < consumeQuantity) {
            onMissingResource.Invoke($"Need {consumeQuantity} {consumedItem.Name} to repair and you only have {count} !");
            return;
        }
        StartCoroutine(Repair());
    }
}
