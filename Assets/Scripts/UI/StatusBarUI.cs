using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusBarUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected BarUI statusBar = null;
    [SerializeField] protected TMPro.TMP_Text statusText = null;
    
    public void UpdateUI(CrewMember crewMember)
    {
        Debug.Log("TODO : status bar");
    }
}
