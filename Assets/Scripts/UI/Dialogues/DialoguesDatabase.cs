using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.U2D;

public class DialoguesDatabase : MonoBehaviour
{

    [Header("Data Files")]
    public SpriteAtlas atlas;
    public TextAsset dialoguesFile;
    public TextAsset charactersFile;

    [Header("Data")]
    public List<Dialogue> Dialogues;
    public List<Character> Characters;

    private StringReader reader;
    private StringReader GetReader(string file)
    {
        if (reader != null)
        {
            reader.Close();
            reader = null;
        }

        reader = new StringReader(file);
        return reader;
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public void CheckInit()
    {
        if (Dialogues == null)
            Init();
    }

    private void Init()
    {
        dialoguesFile = Resources.Load<TextAsset>("dialogues");
        charactersFile = Resources.Load<TextAsset>("characters");

        Dialogues = (List<Dialogue>)new XmlSerializer(Dialogues.GetType())
            .Deserialize(GetReader(dialoguesFile.text));
        Characters = (List<Character>)new XmlSerializer(Characters.GetType())
            .Deserialize(GetReader(charactersFile.text));
    }

    public Dialogue FindDialogue(string id)
    {
        return Dialogues.Find(d => d.identifier.Equals(id));
    }

    public Character FindCharacter(string name)
    {
        return Characters.Find(c => c.name.Equals(name));
    }

    public Sprite FindCharacterSprite(Character c)
    {
        return atlas.GetSprite(c.sprite);
    }
}


[Serializable]
public struct Character
{
    public string name;
    public string sprite;
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