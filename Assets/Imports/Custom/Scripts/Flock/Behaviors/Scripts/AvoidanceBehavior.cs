using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Avoidance")]
public class AvoidanceBehavior : FilteredFlockBehavior
{
    public float frontAvoidForce = 1.5f;
    public float minimalFrontAvoid = -.9f;
    public override Vector3 CalculateMove(in FlockAgent agent, in List<Transform> context)
    {
        List<Transform> newContext = filter != null ? filter.filter(agent, context) : context;
        if (newContext.Count == 0) {
            return Vector3.zero;
        }
        Vector3 avoidanceMove = Vector3.zero;
        int avoidCount = 0;
        foreach (Transform item in newContext)
        {
            Vector3 closestPoint = item.GetComponent<Collider>().ClosestPoint(agent.transform.position);
            if (Vector3.SqrMagnitude(closestPoint - agent.transform.position) < agent.SquareAvoidanceRadius) {
                Vector3 dist = agent.transform.position - closestPoint;
                avoidCount++;
                avoidanceMove += dist.normalized * agent.AvoidanceRadius - dist;
            }
        }
        if (avoidCount != 0) {
            avoidanceMove /= avoidCount;
        }

        return avoidanceMove;
    }
}
