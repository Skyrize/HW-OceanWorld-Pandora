using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/CrewInventory")]
public class CrewInventory : Inventory
{
    public List<CrewMember> crewMembers = new List<CrewMember>();

    override protected ClonableSO Clone()
    {
        CrewInventory clone = base.Clone() as CrewInventory;

        clone.crewMembers.Clear();
        foreach (CrewMember crewMember in crewMembers)
        {
            clone.crewMembers.Add(ClonableSO.Clone<CrewMember>(crewMember));
        }
        return clone;
    }

    public void AddCrewMember(CrewMember newCrewMember)
    {
        if (crewMembers.Find(crewMember => crewMember.Name == newCrewMember.Name)) {
            throw new System.Exception($"Can't add two CrewMembers with the same name '{newCrewMember.Name}' for now");
        }
        crewMembers.Add(ClonableSO.Clone<CrewMember>(newCrewMember));
    }
}
