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
    [SerializeField] protected string _name = "default Name";
    public string Name => _name;
    public List<Skill> skills = new List<Skill>();
    [Header("References")]
    public GameObject prefab;
    public Post currentPost;
    public Sprite icon;
}
