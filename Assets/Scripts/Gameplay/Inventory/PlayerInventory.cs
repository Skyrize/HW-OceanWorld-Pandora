using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Player")]
public class PlayerInventory : Inventory
{
    [SerializeField] protected CrewMember playerCharacter = null;
    public CrewMember PlayerCharacter => playerCharacter;
    public List<CrewMember> crewMembers = new List<CrewMember>();

    override protected ClonableSO Clone()
    {
        PlayerInventory clone = base.Clone() as PlayerInventory;

        clone.crewMembers.Clear();
        clone.playerCharacter = ClonableSO.Clone<CrewMember>(playerCharacter);
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
