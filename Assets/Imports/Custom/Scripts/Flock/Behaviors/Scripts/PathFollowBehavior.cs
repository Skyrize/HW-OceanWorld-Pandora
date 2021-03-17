using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/PathFollow")]
public class PathFollowBehavior : FlockBehavior
{
    public override Vector3 CalculateMove(in FlockAgent agent, in List<Transform> context)
    {
        BoatAgent boatAgent = agent as BoatAgent;

        return boatAgent.GetInput();
    }
}