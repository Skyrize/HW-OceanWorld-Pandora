using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICrewController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var inventory = GetComponent<InventoryHolder>().inventory as CrewInventory;
        var weapons = GetComponent<Weaponry>().weapons;

        int max = Mathf.Min(inventory.crewMembers.Count, weapons.Count);
        for (int i = 0; i != max; i++) {
            weapons[i].SetEmployee(inventory.crewMembers[i]);
        }
    }

}
