using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CrewMember/Skill")]
public class Skill : ScriptableObject
{
    [Header("Settings")]
    [SerializeField] protected string _name = "default Skill Name";
    [SerializeField] protected string description = "default Skill Description Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
    public string Name => _name;
    public string Description => description;

    public override string ToString()
    {
        return Name + " - \"" + Description + "\"";
    }
}
