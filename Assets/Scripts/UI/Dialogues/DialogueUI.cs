using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    [Header("Data & Utils")]
    public DialoguesDatabase database;
    public PauseManager pauseManager;

    [Header("Actors")]
    public GameObject actorPrefab;
    public GameObject left;
    public GameObject right;

    [Header("Bubble")]
    public Text title;
    public Text content;

    private List<Actor> currentActors = new List<Actor>();

    private Dialogue current;
    private int lineIndex = -1;
    
    private Canvas canvas;
    private Action onDialogueFinish;

    void Start()
    {
        canvas = GetComponent<Canvas>();
        GetComponentInChildren<Button>().onClick.AddListener(OnNextLine);
        database.CheckInit();
    }

    public void OnNextLine()
    {
        if (HasNextLine) LineIndex++;
        else Enabled = false;
    }

    private void ScrubActors()
    {
        currentActors.ForEach(actor => Destroy(actor.gameObject));
        currentActors = new List<Actor>();
    }

    private void InstantiateActor(string name, Transform zone)
    {
        GameObject newActor = Instantiate(actorPrefab, zone);
        currentActors.Add(newActor.GetComponent<Actor>());

        currentActors[currentActors.Count - 1].subject = database.FindCharacter(name);
        newActor.SetActive(true);
    }

    public void Summon(string dialogueId)
    {
        onDialogueFinish = null;
        current = database.FindDialogue(dialogueId);
        print(current.charactersLeft);

        ScrubActors();
        current.charactersLeft.ForEach(c => InstantiateActor(c, left.transform));
        current.charactersRight.ForEach(c => InstantiateActor(c, right.transform));

        lineIndex = -1;
        LineIndex++;

        Enabled = true;
    }

    public void Summon(string dialogueId, Action onFinishDialogue)
    {
        Summon(dialogueId);
        onDialogueFinish = onFinishDialogue;
    }

    private bool Enabled
    {
        get => canvas.enabled;
        set
        {
            canvas.enabled = value;

            if (value) pauseManager.Pause();
            else pauseManager.Unpause();

            if (!value && onDialogueFinish != null)
                onDialogueFinish();
        }
    }

    private int LineIndex
    {
        get => lineIndex;
        set
        {
            lineIndex = value;
            title.text = current.lines[lineIndex].name;
            content.text = current.lines[lineIndex].text;

            currentActors.ForEach(actor =>
            {
                actor.GetComponentInChildren<Image>()
                                .color = actor.subject.name.Equals(title.text)
                                ? Color.white
                                : Color.gray;
            });
        }
    }

    private bool HasNextLine => current.lines.Count - 1 > lineIndex;
}