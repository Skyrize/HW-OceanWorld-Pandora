using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryFeedback : MonoBehaviour
{
    [Header("Inventory")]
    public InventoryHolder inventoryHolder;

    public Text AmmoText;

    public uint LowAmmoThreshold = 5;

    [Header("UI Colors")]
    public Gradient NoAmmoGradient;

    public Gradient LowAmmoGradient;

    private void Start()
    {
        AmmoText.text = "";
    }

    private void Update()
    {
        var ammo = CountAmmunition();
        var delta = (Mathf.Cos(6f * Time.time) + 1f) / 2f;

        if (ammo == 0)
        {
            AmmoText.text = "No ammo";
            AmmoText.color = NoAmmoGradient.Evaluate(delta);
        }
        else if (ammo <= LowAmmoThreshold)
        {
            AmmoText.text = "Low ammo";
            AmmoText.color = LowAmmoGradient.Evaluate(delta);
        }
        else
        {
            AmmoText.text = "";
        }
    }

    private int CountAmmunition()
    {
        return (from item in inventoryHolder.inventory.items
            where item.item.Prefab
            where item.item.Prefab.layer == LayerMask.NameToLayer("Ammunition")
            select (int) item.count).Sum();
    }
}