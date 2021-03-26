using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/CrewInventory")]
public class CrewInventory : Inventory
{
    [SerializeField] public uint maxCrewSize = 2;
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
        if (crewMembers.Count == maxCrewSize)
        {
            throw new System.Exception($"You have reached your max crew capacity '{maxCrewSize}'.");
        }
        if (crewMembers.Find(crewMember => crewMember.Name == newCrewMember.Name))
        {
            throw new System.Exception($"Can't add two CrewMembers with the same name '{newCrewMember.Name}' for now");
        }
        crewMembers.Add(ClonableSO.Clone<CrewMember>(newCrewMember));
    }

    public void RemoveCrewMember(CrewMember toRemove)
    {
        var fdp = crewMembers.Find(crew => crew.Name.Equals(toRemove.Name));
        try { fdp.Quit(); } catch { }
        if (!crewMembers.Remove(fdp))
        {
            throw new System.Exception($"Can’t remove member '{toRemove.Name}' not in crew");
        }
    }
}
