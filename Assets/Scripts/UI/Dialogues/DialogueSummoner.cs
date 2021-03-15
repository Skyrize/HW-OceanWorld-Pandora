using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSummoner : MonoBehaviour
{
    [Header("Dialogues")]
    public DialogueUI ui;
    public DialogueIdentifier dialogue;

    [Header("Merchant")]
    public Merchant merchant;

    [Header("Crew adding")]
    public CrewMember suit;
    public InventoryHolder inventoryHolder;

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
                    ui.Summon("first_fight"); //todo add post summon ?
                    break;
                case DialogueIdentifier.AFTER_FIGHT:
                    ui.Summon("after_fight"); //todo add post summon ?
                    break;
            }
    }

    private void AddSuitToCrew()
    {
        ((PlayerInventory)inventoryHolder.inventory).AddCrewMember(suit);
        gameObject.SetActive(false);
    }

    private void OnSuitBroughtBack()
    {
        dialogue = DialogueIdentifier.MERCHANT;
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