using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Team
{
    PLAYER,
    AI
}

public class TeamManager : MonoBehaviour
{
    public Team team = Team.PLAYER;

    private void Start() {
        foreach (BoatAgent boat in transform.GetComponentsInChildren<BoatAgent>())
        {
            // boat.Team = team;
        }    
    }
}
