using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Filter/SameFlockFilter")]
public class SameFlockFilter : ContextFilter
{
    override public List<Transform> filter(in FlockAgent agent, in List<Transform> originalContext)
    {
        List<Transform> filtered = new List<Transform>();

        foreach (Transform item in originalContext)
        {
            FlockAgent itemAgent = item.GetComponentInParent<FlockAgent>();

            if (itemAgent != null && itemAgent.flock == agent.flock) {
                filtered.Add(item);
            }
        }

        return filtered;
    }
}
