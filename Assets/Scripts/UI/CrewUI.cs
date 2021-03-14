using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewUI : MonoBehaviour
{
    // [Header("Events")]
    // [SerializeField] public CrewMemberEvent onSelect = new CrewMemberEvent();
    [Header("References")]
    [HideInInspector] protected PlayerInventory playerInventory = null;
    [SerializeField] protected RectTransform CrewPanelContent = null;
    [SerializeField] protected GameObject CrewMemberCardPrefab = null;
    [Header("Runtime")]
    [SerializeField] protected CrewMember currentCrewMember = null;
    [SerializeField] public CrewMember CurrentCrewMember => currentCrewMember;
    // [SerializeField] List<CrewMemberUI> crewMemberUIs = new List<CrewMemberUI>();

    public void GetPlayerInventory()
    {
        var player = GameObject.FindObjectOfType<Player>();

        if (player) {
            this.playerInventory = player.inventory;
        } else {
            throw new MissingReferenceException("No Player in scene to hold an inventory. It is need to ensure you don't modify an asset while using inventory.");
        }
    }

    private void Awake() {
        GetPlayerInventory();
    }
    
    public void UnselectCrewMember(CrewMember crewMember) // need that ?
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        currentCrewMember = null;
    }

    public void SelectCrewMember(CrewMember crewMember)
    {
        Cursor.SetCursor(crewMember.icon.texture, Vector2.zero, CursorMode.Auto);
        currentCrewMember = crewMember;
    }

    public void CreateCard(CrewMember crewMember)
    {
        CrewMemberUI cardUI = GameObject.Instantiate(CrewMemberCardPrefab, CrewPanelContent).GetComponent<CrewMemberUI>();

        cardUI.UpdateUI(crewMember);
        cardUI.onSelect.AddListener(this.SelectCrewMember);
        cardUI.onUnselect.AddListener(this.UnselectCrewMember);
    }

    public void ClearUI()
    {
        CrewPanelContent.ClearChilds(); // TODO: easy but dirty. Maybe remove them along when adding to inventory
    }
    
    public void BuildUI()
    {
        CreateCard(playerInventory.PlayerCharacter);
        foreach (CrewMember crewMember in playerInventory.crewMembers)
        {
            CreateCard(crewMember);
        }
    }

    private void OnEnable() {
        BuildUI();
    }

    private void OnDisable() {
        ClearUI();
    }
}
