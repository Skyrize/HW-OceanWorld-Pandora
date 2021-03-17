using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Behavior
{
    public FlockBehavior behavior;
    public float weight;
    public float max;
}

[CreateAssetMenu(menuName = "Flock/Behavior/Composite")]
public class CompositeBehavior : FlockBehavior
{
    [SerializeField] protected Behavior[] behaviors;
    public override Vector3 CalculateMove(in FlockAgent agent, in List<Transform> context)
    {
        Vector3 result = Vector3.zero;

        foreach (Behavior item in behaviors)
        {
            Vector3 move = item.behavior.CalculateMove(agent, context) * item.weight;

            if (move.sqrMagnitude != 0 && move.sqrMagnitude > item.max * item.max) {
                move = move.normalized * item.max;
            }
            result += move;
        }

        return result;
    }
}
