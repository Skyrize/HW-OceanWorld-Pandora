using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    [Header("Data")]
    public DialoguesDatabase database;

    [Header("Actors")]
    public GameObject actorPrefab;
    public GameObject left;
    public GameObject right;

    [Header("Bubble")]
    public Text title;
    public Text content;

    private Dialogue current;
    private int lineIndex = -1;

    private float delayNext = .5f;
    private float lastNext;
    private bool CanNext => lastNext >= delayNext;

    // Start is called before the first frame update
    void Start()
    {
        Summon("exemple");
    }

    // Update is called once per frame
    void Update()
    {
        if (!CanNext) { 
            lastNext += Time.deltaTime; 
            return;
        }

        if (Input.GetButton("Fire1") && HasNextLine)
        {
            LineIndex++;
            lastNext = 0;
        }
        else if (Input.GetButton("Fire1") && !HasNextLine)
            gameObject.SetActive(false);

        if (Input.GetButton("Fire2"))
            Summon("exemple");
    }

    private int LineIndex
    {
        get => lineIndex; 
        set
        {
            lineIndex++;
            title.text = current.lines[lineIndex].name;
            content.text = current.lines[lineIndex].text;
        }
    }

    private void ScrubActors()
    {
        var toRemove = new List<UnityEngine.Object>();
        foreach (Transform a in left.transform)
            toRemove.Add(a.gameObject);
        foreach (Transform a in right.transform)
            toRemove.Add(a.gameObject);

        toRemove.ForEach(a => Destroy(a));
    }

    private void InstantiateActor(string name, Transform zone)
    {
        GameObject newActor = Instantiate(actorPrefab, zone);

        newActor.GetComponent<Actor>().subject = database.FindCharacter(name);
        newActor.SetActive(true);
    }

    public void Summon(string dialogueId)
    {
        current = database.FindDialogue(dialogueId);

        ScrubActors();

        current.charactersLeft.ForEach(c => InstantiateActor(c, left.transform));
        current.charactersRight.ForEach(c => InstantiateActor(c, right.transform));

        lineIndex = -1;
        LineIndex++;
    }

    private bool HasNextLine => current.lines.Count - 1 > lineIndex;
}