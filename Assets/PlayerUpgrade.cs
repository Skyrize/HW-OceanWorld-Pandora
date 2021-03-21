using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Upgrade {
    public string name = "Default";
    public float newHPMax = 100f;
    public uint newCrewSize = 2;

    public GameObject boatMesh;

    public Vector3 motorPlacement = Vector3.zero;
    public Vector3 controlPostPlacement = Vector3.zero;
    public Vector3 repairStationPlacement = Vector3.zero;

    public Vector3[] flamesPlacement = new Vector3[4];
    
    public GameObject boatRipple = null;

    public Vector3 avoidanceCenter;
    public float avoidanceHeight;
    public float avoidanceRadius;

}

public class PlayerUpgrade : MonoBehaviour
{
    [Header("Upgrades")]
    public Upgrade[] upgrades;

    [Header("References")]
    public Transform boatMeshParent = null;
    public Transform motor = null;
    public Transform controlPost = null;
    public Transform repairStation = null;
    public Transform motorRipple = null;
    public GameObject boatRippleStart = null;
    public FlamesManager flameManager = null;
    public CapsuleCollider avoidanceCollider = null;
    public HealthComponent health = null;
    public InventoryHolder inventoryHolder = null;

    int currentUpgrade = 0;

    void SetBoatMesh()
    {
        boatMeshParent.ClearChilds();
        Instantiate(upgrades[currentUpgrade].boatMesh, boatMeshParent);
    }

    void SetFlames()
    {
        flameManager.ReplaceFlames(upgrades[currentUpgrade].flamesPlacement);
    }

    void SetRipple()
    {
        motorRipple.localPosition = upgrades[currentUpgrade].motorPlacement;
        if (boatRippleStart.activeInHierarchy) boatRippleStart.SetActive(false);
        upgrades[currentUpgrade].boatRipple.SetActive(true);
    }

    void SetPosts()
    {
        motor.localPosition = upgrades[currentUpgrade].motorPlacement;
        controlPost.localPosition = upgrades[currentUpgrade].controlPostPlacement;
        repairStation.localPosition = upgrades[currentUpgrade].repairStationPlacement;
    }

    void SetAvoidance()
    {
        avoidanceCollider.center = upgrades[currentUpgrade].avoidanceCenter;
        avoidanceCollider.height = upgrades[currentUpgrade].avoidanceHeight;
        avoidanceCollider.radius = upgrades[currentUpgrade].avoidanceRadius;
    }

    void SetCrewSize()
    {
        PlayerInventory inventory = inventoryHolder.inventory as PlayerInventory;
        inventory.maxCrewSize = upgrades[currentUpgrade].newCrewSize;
    }
    void SetMaxHP()
    {
        health.SetMaxHealth(upgrades[currentUpgrade].newHPMax); // In last for event invoke
    }

    public int Upgrade()
    {
        if (currentUpgrade == upgrades.Length)
            return currentUpgrade - 1;
        SetBoatMesh();
        SetFlames();
        SetRipple();
        SetPosts();
        SetAvoidance();
        SetCrewSize();
        SetMaxHP();
        currentUpgrade++;
        return currentUpgrade;
    }
}
