using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Actor : MonoBehaviour
{
    public DialoguesDatabase database;
    public Character subject;

    // Start is called before the first frame update
    void Start()
    {
        GetComponentInChildren<Image>().sprite = database.FindCharacterSprite(subject);
    }

    public void Dispose()
    {
        Destroy(this);
    }
}
