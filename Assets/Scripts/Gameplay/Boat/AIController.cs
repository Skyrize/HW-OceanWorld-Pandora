using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

enum DIRECTION
{
    LEFT,
    RIGHT,
    NONE
}

public class AIController : Controller
{
    [Header("AI")]
    public float multiplier = 1f;
    public float minimalSpeed = 1f;
    public float stoppingDistance = 1f;
    private float squareMinimalSpeed = 1f;
    private float squareStoppingDistance = 1f;

    // private NavMeshAgent agent;
    private Avoidance avoid;
    private Rigidbody rb;

    private NavMeshPath path;
    // private int currentCornerIdx = 0;
    private bool destinationReached = true;
    private Vector3? lastTarget;

    private DIRECTION rotatingSide = DIRECTION.NONE;

    private void Awake() {
        // agent = GetComponent<NavMeshAgent>();
        avoid = GetComponent<Avoidance>();
        rb = GetComponent<Rigidbody>();
        squareMinimalSpeed = minimalSpeed * minimalSpeed;
        squareStoppingDistance = stoppingDistance * stoppingDistance;
        path = new NavMeshPath();
    }

    bool HasArrived()
    {
        if (path.corners.Length == 0)
            return true;
        return (path.corners[path.corners.Length - 1] - transform.position).sqrMagnitude < squareStoppingDistance;
    }

    void checkNextCorner(bool shouldUpdateInputManagement)
    {
        // while ((path.corners[currentCornerIdx] - transform.position).magnitude < agent.stoppingDistance)
        // {
        //     currentCornerIdx += 1;
        //     if (currentCornerIdx >= path.corners.Length)
        //     {
        //         destinationReached = true;
        //         return;
        //     } else if (shouldUpdateInputManagement)
        //     {
        //         updateInputManagement();
        //     }
        // }
    }

    public void setTarget(Vector3 target)
    {
        if (lastTarget.HasValue && target == lastTarget)
        {
            return;
        }
        NavMeshHit hit;
        if (NavMesh.SamplePosition(target, out hit, 1000f, NavMesh.AllAreas)) {
            target = hit.position;
            Debug.DrawLine(target, target + Vector3.up * 100f, Color.cyan, 0.5f);
        }
        lastTarget = target;
        // currentCornerIdx = 0;
        // if (!agent.CalculatePath(target, path)) {
        if (!NavMesh.CalculatePath(transform.position, target, NavMesh.AllAreas, path)) {
            Debug.LogError("Failed to calculate path for AIController");
        }
        destinationReached = HasArrived();
        // destinationReached = false;
        // // checkNextCorner(false);
        // if (!destinationReached)
        // {
        //     updateInputManagement();
        // }
    }
    void updatePath()
    {
        if (path.status == NavMeshPathStatus.PathComplete || path.status == NavMeshPathStatus.PathPartial)
        {
            destinationReached = HasArrived();
            // checkNextCorner(true);
        }
    }

    void updateInputManagement()
    {
        bool needToGoBackward = getRelativeDirection().z < 0f;
        if (!needToGoBackward)
        {
            rotatingSide = DIRECTION.NONE;
        }
    }

    private Vector3 getRelativeDirection()
    {
        if (path.corners.Length == 0)
            return Vector3.forward;
        return transform.InverseTransformPoint(path.corners[1]);
    }

    public override Vector3 getInput()
    {
        if (destinationReached)
        {
            return new Vector3(0, 0, 0);
        }
        Vector3 avoidanceInput = avoid.CalculateMove();
        Vector3 simulatedInput = getRelativeDirection().normalized;
        simulatedInput.y = 0;
        if (simulatedInput.z <= 0) {
            simulatedInput.x = 1f * Mathf.Sign(simulatedInput.x);

        }
        // float dot = Vector3.Dot(simulatedInput, Vector3.forward);
        // if (dot < 0) {
        //     simulatedInput.x = 1f * Mathf.Sign(simulatedInput.x);
        // }
        // if (simulatedInput.z < 0) // we need to go backward
        // {
        //     if (rotatingSide == DIRECTION.NONE)
        //     {
        //         rotatingSide = simulatedInput.x < 0 ? DIRECTION.LEFT : DIRECTION.RIGHT;
        //     }
        //     simulatedInput.x = rotatingSide == DIRECTION.RIGHT ? 1 : -1;
        //     simulatedInput.z = 0;
        // }
        if (rb.velocity.sqrMagnitude < squareMinimalSpeed)
        {
            simulatedInput.z = 1f;
        }

        Debug.DrawRay(transform.position, transform.TransformDirection(simulatedInput + avoidanceInput), Color.black, Time.deltaTime);
        return (simulatedInput + avoidanceInput).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        if (destinationReached)
        {
            return;
        }
        for (int i = 0; i < path.corners.Length - 1; i++) {
            Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red, Time.deltaTime);
        }
        updatePath();
    }
}
