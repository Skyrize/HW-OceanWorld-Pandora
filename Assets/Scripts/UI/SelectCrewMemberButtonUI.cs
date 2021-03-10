using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectCrewMemberButtonUI : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] public CrewMemberEvent onSelect = new CrewMemberEvent();
    [SerializeField] public CrewMemberEvent onUnselect = new CrewMemberEvent();
    [Header("References")]
    [SerializeField] protected Image icon;
    [Header("Runtime")]
    [SerializeField] protected CrewMember crewMember;
    public void UpdateUI(CrewMember crewMember)
    {
        this.crewMember = crewMember;
        icon.sprite = this.crewMember.icon;
    }

    public void Select()
    {
        onSelect.Invoke(this.crewMember);
    }

    public void Unselect()
    {
        onUnselect.Invoke(this.crewMember);
    }
}
