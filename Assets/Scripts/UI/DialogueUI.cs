using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    [Header("Data")]
    public List<Character> characters;
    public List<Dialogue> dialogues;

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
        List<GameObject> toRemove = new List<GameObject>();
        foreach (Transform child in left.transform) toRemove.Add(child.gameObject);
        foreach (Transform child in right.transform) toRemove.Add(child.gameObject);

        toRemove.ForEach(g => Destroy(g));
    }

    private void InstantiateActor(string name, bool isOnLeft)
    {
        GameObject newActor = Instantiate(actorPrefab, isOnLeft ? left.transform : right.transform);
        
        Image newActorSprite = newActor.GetComponentInChildren<Image>();
        newActorSprite.sprite = FindCharacter(name).sprite;

        newActor.SetActive(true);
    }

    public void Summon(string dialogueId)
    {
        current = FindDialogue(dialogueId);

        ScrubActors();

        current.charactersLeft.ForEach(c => InstantiateActor(c, true));
        current.charactersRight.ForEach(c => InstantiateActor(c, false));

        lineIndex = -1;
        LineIndex++;
    }

    private Dialogue FindDialogue(string id)
    {
        return dialogues.Find(d => d.identifier.Equals(id));
    }

    private Character FindCharacter(string name)
    {
        return characters.Find(c => c.name.Equals(name));
    }

    private bool HasNextLine => current.lines.Count - 1 > lineIndex;
}

[Serializable]
public struct Character
{
    public string name;
    public Sprite sprite;
}

[Serializable]
public struct Dialogue
{
    public string identifier;
    public List<string> charactersLeft;
    public List<string> charactersRight;
    public List<Line> lines;
}

[Serializable]
public struct Line
{
    public string name;
    public string text;
}