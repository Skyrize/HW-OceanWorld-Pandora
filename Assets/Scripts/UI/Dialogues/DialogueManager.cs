using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogues")]
    public DialogueUI ui;
    public DialogueIdentifier dialogue;

    [Header("Suit Pickup")]
    public CrewManager crewManager;

    [Header("Progress check")]
    public HealthComponent firstFoe;
    public List<HealthComponent> foes;
    public GameObject door;
    private bool ready = false;
    private int accumulator = 0;

    [Header("Merchant")]
    public Merchant merchant;

    [Header("Crew management")]
    public CrewMember crew;
    public InventoryHolder inventoryHolder;

    private void Start()
    {
        if (firstFoe != null)
            firstFoe.onDeathEvent.AddListener(PrepareAfterFight);

        if (foes != null)
            foes.ForEach(foe => foe.onDeathEvent.AddListener(FoeElimination));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Utils.Tags.PLAYER))
            switch (dialogue)
            {
                case DialogueIdentifier.SUIT_BACK:
                    ui.Summon("quarter_1", OnSuitBroughtBack);
                    break;
                case DialogueIdentifier.QUARTERMASTER:
                    ui.Summon("quartermaster", merchant.EnterInMerchant);
                    break;
                case DialogueIdentifier.MERCHANT:
                    ui.Summon("merchant", merchant.EnterInMerchant);
                    break;
                case DialogueIdentifier.INTRODUCTION:
                    ui.Summon("introduction", AddCrewMember);
                    break;
                case DialogueIdentifier.OLDS_SPEAKING:
                    ui.Summon("olds_talking", () => Destroy(this));
                    break;
                case DialogueIdentifier.DOOR_BLOCKED:
                    ui.Summon("door_blocked");
                    break;
                case DialogueIdentifier.FISHERMAN:
                    ui.Summon("fisherman");
                    break;
                case DialogueIdentifier.ARK_GUARD:
                    if (accumulator == foes.Count) 
                        ui.Summon("ark_opened", () => Destroy(gameObject));
                    else ui.Summon("ark_closed");
                    break;
                case DialogueIdentifier.FIRST_FIGHT:
                    ui.Summon("first_fight", () => { crewManager.Enter(); Destroy(this); });
                    break;
                case DialogueIdentifier.RECRUIT_BATMAN:
                    ui.Summon("recruit_batman", () => { AddCrewMember(); Destroy(door); });
                    break;
                case DialogueIdentifier.AFTER_FIGHT:
                    if (!ready) break;
                    ui.Summon("after_fight", () => ui.Summon("after_fight_suit", () => Destroy(gameObject)));
                    break;
            }
    }

    private void PrepareAfterFight(GameObject go)
    {
        GetComponent<ParticleSystem>().Stop();
        GetComponent<BoxCollider>().isTrigger = true;
        ready = true;
    }

    private void FoeElimination(GameObject go)
    {
        accumulator++;
    }

    private void AddCrewMember()
    {
        try
        {
            ((PlayerInventory)inventoryHolder.inventory)
                .AddCrewMember(crew);
        }
        catch { print("must not forget to not give any crew member to player so suit doesnt overload the limit"); }

        Destroy(gameObject);
    }

    private void OnSuitBroughtBack()
    {
        dialogue = DialogueIdentifier.QUARTERMASTER;
        ((PlayerInventory)inventoryHolder.inventory)
            .RemoveCrewMember(crew);
        ui.Summon("quarter_2", merchant.EnterInMerchant);
    }
}

public enum DialogueIdentifier
{
    INTRODUCTION,
    FIRST_FIGHT,
    AFTER_FIGHT,
    SUIT_BACK,
    RECRUIT_BATMAN,
    DOOR_BLOCKED,
    OLDS_SPEAKING,
    QUARTERMASTER,
    MERCHANT,
    ARK_GUARD,
    FISHERMAN,
}