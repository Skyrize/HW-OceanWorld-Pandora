using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatFlock : Flock
{
    override public void UpdateAgent(FlockAgent agent)
    {
            List<Transform> context = agent.GetNearbyObstacles();
            Vector3 move = behavior.CalculateMove(agent, context);
            agent.Move(move);
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateAgents();
    }
}
