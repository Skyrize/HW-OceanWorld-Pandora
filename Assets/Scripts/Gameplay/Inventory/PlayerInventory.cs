using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Player")]
public class PlayerInventory : Inventory<InventoryStorage>
{
    public List<InventoryStorage> items = new List<InventoryStorage>();
    public List<CrewMember> crewMembers = new List<CrewMember>();
    override public List<InventoryStorage> m_content { get { return items; } }

    override protected ClonableSO Clone()
    {
        PlayerInventory clone = base.Clone() as PlayerInventory;

        clone.crewMembers.Clear();
        foreach (CrewMember crewMember in crewMembers)
        {
            clone.crewMembers.Add(ClonableSO.Clone<CrewMember>(crewMember));
        }
        return clone;
    }
}
