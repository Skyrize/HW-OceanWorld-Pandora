using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Cohesion")]
public class CohesionBehavior : FilteredFlockBehavior
{
    public override Vector3 CalculateMove(in FlockAgent agent, in List<Transform> context)
    {
        List<Transform> newContext = filter != null ? filter.filter(agent, context) : context;
        if (newContext.Count == 0) {
            return Vector3.zero;
        }
        Vector3 cohesionMove = Vector3.zero;

        foreach (Transform item in newContext)
        {
            cohesionMove += item.position;
        }
        cohesionMove /= newContext.Count;

        cohesionMove -= agent.transform.position;
        return cohesionMove;
    }
}
