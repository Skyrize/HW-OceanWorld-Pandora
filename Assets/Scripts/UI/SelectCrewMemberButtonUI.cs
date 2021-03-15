using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectCrewMemberButtonUI : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] public CrewMemberEvent onSelect = new CrewMemberEvent();
    [Header("References")]
    [SerializeField] protected Image icon;
    [Header("Runtime")]
    [SerializeField] protected CrewMember crewMember;
    public void UpdateUI(CrewMember crewMember)
    {
        this.crewMember = crewMember;
        icon.sprite = this.crewMember != null ? this.crewMember.icon : null;
    }

    public void Select()
    {
        onSelect.Invoke(this.crewMember);
    }
}
