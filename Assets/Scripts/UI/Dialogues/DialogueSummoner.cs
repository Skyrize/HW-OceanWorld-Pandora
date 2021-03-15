using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSummoner : MonoBehaviour
{
    public DialogueUI ui;
    public Merchant merchant;
    public DialogueIdentifier dialogue;

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
            }
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