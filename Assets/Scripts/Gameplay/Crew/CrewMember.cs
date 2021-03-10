using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CrewMemberEvent : UnityEvent<CrewMember>
{
}


[CreateAssetMenu(menuName = "CrewMember/CrewMember")]
public class CrewMember : ClonableSO
{
    [Header("Settings")]
    public string Name = "default Name";
    public List<Skill> skills = new List<Skill>();
    [Header("References")]
    public GameObject prefab;
    public Sprite icon;
}
