using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICrewController : MonoBehaviour
{
    [SerializeField] protected RepairStation repairStation = null;
    [SerializeField] protected Post controlStation = null;
    [SerializeField] protected CrewInventory inventory = null;

    public void Assign(List<Post> posts)
    {
        int max = Mathf.Min(inventory.crewMembers.Count, posts.Count);

        for (int i = 0; i != max; i++) {
            posts[i].ForceHire(inventory.crewMembers[i]);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        inventory = GetComponent<InventoryHolder>().inventory as CrewInventory;
        var weapons = GetComponent<Weaponry>().weapons;
        

    }

    private void Update() {
        if (repairStation)
            repairStation.Use();
    }

}
