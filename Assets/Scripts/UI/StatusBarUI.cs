using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusBarUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected BarUI statusBar = null;
    [SerializeField] protected TMPro.TMP_Text statusText = null;

    protected CrewMember currentCrewMember = null;
    
    public void UpdateUI(CrewMember crewMember)
    {
        currentCrewMember = crewMember;
    }

    private void Update() {
        if (currentCrewMember) {
            statusBar.UpdateRatio(currentCrewMember.HireRatio);
            statusText.text = currentCrewMember.Status;
        } else {
            statusText.text = "Crewmate needed";
        }
    }
}
