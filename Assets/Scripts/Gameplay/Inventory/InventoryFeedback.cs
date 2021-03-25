using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryFeedback : MonoBehaviour
{
    [Header("Inventory")]
    public InventoryHolder inventoryHolder;

    public Text CannonAmmoText;
    public Text BulletAmmoText;

    public uint LowAmmoThreshold = 5;

    [Header("UI Colors")]
    public Gradient NoAmmoGradient;

    public Gradient LowAmmoGradient;

    private bool m_hadBullets;
    private bool m_hadCannonBalls;

    private void Awake()
    {
        CannonAmmoText.text = "";
        BulletAmmoText.text = "";
    }

    private void Update()
    {
        var ammoList = CountAmmunition();
        var delta = (Mathf.Cos(6f * Time.time) + 1f) / 2f;
        var bullets = 0u;
        var cannonBalls = 0u;

        foreach (var ammo in ammoList)
        {
            if (ammo.Key.ToLower().Contains("bullet"))
            {
                bullets += ammo.Value;
                m_hadBullets = true;
            }
            else if (ammo.Key.ToLower().Contains("cannonball"))
            {
                cannonBalls += ammo.Value;
                m_hadCannonBalls = true;
            }
        }

        if (m_hadBullets)
        {
            if (bullets == 0)
            {
                BulletAmmoText.text = "No bullets";
                BulletAmmoText.color = NoAmmoGradient.Evaluate(delta);
            }
            else if (bullets <= LowAmmoThreshold)
            {
                BulletAmmoText.text = "Low bullets";
                BulletAmmoText.color = LowAmmoGradient.Evaluate(delta);
            }
            else
            {
                BulletAmmoText.text = "";
            }
        }

        if (m_hadCannonBalls)
        {
            if (cannonBalls == 0)
            {
                CannonAmmoText.text = "No cannon balls";
                CannonAmmoText.color = NoAmmoGradient.Evaluate(delta);
            }
            else if (cannonBalls <= LowAmmoThreshold)
            {
                CannonAmmoText.text = "Low cannon balls";
                CannonAmmoText.color = LowAmmoGradient.Evaluate(delta);
            }
            else
            {
                CannonAmmoText.text = "";
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