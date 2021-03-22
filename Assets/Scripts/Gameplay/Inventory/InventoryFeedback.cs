using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryFeedback : MonoBehaviour
{
    [Header("Inventory")]
    public InventoryHolder inventoryHolder;

    public Text ConnonAmmoText;
    public Text BulletAmmoText;

    public uint LowAmmoThreshold = 5;

    [Header("UI Colors")]
    public Gradient NoAmmoGradient;

    public Gradient LowAmmoGradient;

    private void Awake()
    {
        ConnonAmmoText.text = "";
        BulletAmmoText.text = "";
    }

    private void Update()
    {
        var ammoList = CountAmmunition();
        var delta = (Mathf.Cos(6f * Time.time) + 1f) / 2f;

        foreach (var ammo in ammoList)
        {
            if (ammo.Key.ToLower().Contains("bullet"))
            {
                if (ammo.Value == 0)
                {
                    BulletAmmoText.text = "No bullets";
                    BulletAmmoText.color = NoAmmoGradient.Evaluate(delta);
                }
                else if (ammo.Value <= LowAmmoThreshold)
                {
                    BulletAmmoText.text = "Low bullets";
                    BulletAmmoText.color = LowAmmoGradient.Evaluate(delta);
                }
                else
                {
                    BulletAmmoText.text = "";
                }
            }
            else
            {
                if (ammo.Value == 0)
                {
                    ConnonAmmoText.text = "No cannon balls";
                    ConnonAmmoText.color = NoAmmoGradient.Evaluate(delta);
                }
                else if (ammo.Value <= LowAmmoThreshold)
                {
                    ConnonAmmoText.text = "Low cannon balls";
                    ConnonAmmoText.color = LowAmmoGradient.Evaluate(delta);
                }
                else
                {
                    ConnonAmmoText.text = "";
                }
            }
        }
    }

    private Dictionary<string, uint> CountAmmunition()
    {
        return inventoryHolder.inventory.items
            .Where(item => item.item.Prefab)
            .Where(item => item.item.Prefab.layer == LayerMask.NameToLayer("Ammunition"))
            .ToDictionary(item => item.item.Name.Replace("(Clone)", ""), item => item.count);
    }
}