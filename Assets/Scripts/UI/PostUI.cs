using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostUI : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] public CrewMemberEvent onSelect = new CrewMemberEvent();
    [SerializeField] public CrewMemberEvent onUnselect = new CrewMemberEvent();
    [Header("References")]
    [SerializeField] protected SelectCrewMemberButtonUI button = null;
    [SerializeField] protected StatusBarUI statusBar = null;
    [SerializeField] protected TMPro.TMP_Text nameText = null;
    [SerializeField] protected TMPro.TMP_Text skillsText = null;
    [Header("Runtime")]
    [SerializeField] protected CrewMember currentCrewMember = null;
    [SerializeField] protected Item postItem = null;

    public void UpdateUI(Item postItem)
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
