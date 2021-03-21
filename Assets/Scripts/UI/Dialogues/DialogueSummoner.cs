using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSummoner : MonoBehaviour
{
    [Header("Dialogues")]
    public DialogueUI ui;
    public DialogueIdentifier dialogue;

    [Header("Suit Pickup")]
    public CrewManager crewManager;

    [Header("Progress check")]
    public HealthComponent firstFoe;
    private bool ready = false;

    [Header("Merchant")]
    public Merchant merchant;

    [Header("Crew management")]
    public CrewMember suit;
    public InventoryHolder inventoryHolder;

    private void Start()
    {
        if (firstFoe != null)
            firstFoe.onDeathEvent.AddListener(PrepareAfterFight);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Utils.Tags.PLAYER))
            switch (dialogue)
            {
                case DialogueIdentifier.SUIT_BACK:
                    ui.Summon("merchant_1", OnSuitBroughtBack);
                    break;
                case DialogueIdentifier.MERCHANT:
                    ui.Summon("merchant", merchant.EnterInMerchant);
                    break;
                case DialogueIdentifier.INTRODUCTION:
                    ui.Summon("introduction", AddSuitToCrew);
                    break;
                case DialogueIdentifier.FIRST_FIGHT:
                    ui.Summon("first_fight", () => { crewManager.Enter(); Destroy(this); });
                    break;
                case DialogueIdentifier.AFTER_FIGHT:
                    if (!ready) break;
                    ui.Summon("after_fight", () => Destroy(this));
                    break;
            }
    }

    private void PrepareAfterFight(GameObject go)
    {
        GetComponent<ParticleSystem>().Stop();
        GetComponent<BoxCollider>().isTrigger = true;
        ready = true;
    }

    private void AddSuitToCrew()
    {
        try
        {
            ((PlayerInventory)inventoryHolder.inventory)
                .AddCrewMember(suit);
        }
        catch { print("must not forget to not give any crew member to player so suit doesnt overload the limit"); }

        Destroy(gameObject);
    }

    private void OnSuitBroughtBack()
    {
        dialogue = DialogueIdentifier.MERCHANT;
        ((PlayerInventory)inventoryHolder.inventory)
            .RemoveCrewMember(suit);
        ui.Summon("merchant_2", merchant.EnterInMerchant);
    }
}

public enum DialogueIdentifier
{
    INTRODUCTION,
    FIRST_FIGHT,
    AFTER_FIGHT,
    SUIT_BACK,
    MERCHANT,
}