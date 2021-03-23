using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDialogue : MonoBehaviour
{

    [Header("Dialogues")]
    public DialogueUI ui;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(Utils.Tags.PLAYER))
            ui.Summon("start", Second);
    }

    private void Second()
    {
        ui.Summon("start_2", () => Destroy(gameObject));
    }

}
