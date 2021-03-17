using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Alignment")]
public class AlignmentBehavior : FilteredFlockBehavior
{
    public override Vector3 CalculateMove(in FlockAgent agent, in List<Transform> context)
    {
        List<Transform> newContext = filter != null ? filter.filter(agent, context) : context;
        if (newContext.Count == 0) {
            return Vector3.zero;
        }
        Vector3 alignmentMove = Vector3.zero;

        foreach (Transform item in newContext)
        {
            alignmentMove += item.forward;
        }
        alignmentMove /= newContext.Count;

        return alignmentMove;
    }
}
