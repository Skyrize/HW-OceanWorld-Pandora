using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSummoner : MonoBehaviour
{
    public DialogueUI ui;
    public string dialogue;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            ui.Summon(dialogue, LaunchSecond);
    }

    private void LaunchSecond()
    {
        ui.Summon("first_attack");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Utils.Tags.PLAYER))
            ui.Summon(dialogue);
    }
}
