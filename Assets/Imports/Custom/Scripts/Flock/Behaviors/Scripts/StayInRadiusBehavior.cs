using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/StayInRadiusBehavior")]
public class StayInRadiusBehavior : FlockBehavior
{
    [SerializeField] public Vector3 centerPoint;
    [Range(1f, 500f)]
    [SerializeField] public float radius = 150f;
    [Range(1f, 500f)]
    [SerializeField] public float pullForce = 10f;

    public override Vector3 CalculateMove(in FlockAgent agent, in List<Transform> context)
    {
        Vector3 centerOffset = centerPoint - agent.transform.position;
        float distanceRatio = centerOffset.magnitude / radius;
        if (distanceRatio < 1f) {
            return Vector2.zero;
        }

        return centerOffset * distanceRatio * distanceRatio;
    }
}
