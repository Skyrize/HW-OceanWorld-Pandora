using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CrewMemberUI : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] public CrewMemberEvent onSelect = new CrewMemberEvent();
    [SerializeField] public UnityEvent onUnselect = new UnityEvent();
    [Header("References")]
    [SerializeField] protected SelectCrewMemberButtonUI button = null;
    [SerializeField] protected StatusBarUI statusBar = null;
    [SerializeField] protected TMPro.TMP_Text nameText = null;
    [SerializeField] protected TMPro.TMP_Text skillsText = null;
    [Header("Runtime")]
    [SerializeField] protected CrewMember crewMember = null;


    public CrewMember CrewMember { get { return crewMember; } }

    private void Start() {
        button.onSelect.AddListener(Select);
        button.onUnselect.AddListener(Unselect);
    }

    public void UpdateUI(CrewMember crewMember)
    {
        this.crewMember = crewMember;
        button.UpdateUI(this.crewMember);
        statusBar.UpdateUI(this.crewMember);
        nameText.text = "Name : " + this.crewMember.Name;
        skillsText.text = "Skills :\n" + string.Join(",\n", this.crewMember.skills);
    }

    public void Select(CrewMember crewMember)
    {
        this.onSelect.Invoke(crewMember);
    }

    public void Unselect()
    {
        this.onUnselect.Invoke();
    }

}
