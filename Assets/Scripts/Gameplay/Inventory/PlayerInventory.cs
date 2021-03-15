using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Player")]
public class PlayerInventory : CrewInventory
{
    [SerializeField] protected CrewMember playerCharacter = null;
    public CrewMember PlayerCharacter => playerCharacter;

    override protected ClonableSO Clone()
    {
        PlayerInventory clone = base.Clone() as PlayerInventory;

        clone.playerCharacter = ClonableSO.Clone<CrewMember>(playerCharacter);
        return clone;
    }
}
