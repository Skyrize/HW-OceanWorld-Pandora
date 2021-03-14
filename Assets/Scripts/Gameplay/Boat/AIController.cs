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
    public float minimalSpeed = 1f;

    [Header("References")]
    public NavMeshAgent agent;

    private NavMeshPath path;
    private int currentCornerIdx = 0;
    private bool destinationReached = true;
    private Vector3? lastTarget;

    private DIRECTION rotatingSide = DIRECTION.NONE;

    void checkNextCorner(bool shouldUpdateInputManagement)
    {
        while ((path.corners[currentCornerIdx] - transform.position).magnitude < agent.stoppingDistance)
        {
            currentCornerIdx += 1;
            if (currentCornerIdx >= path.corners.Length)
            {
                destinationReached = true;
                return;
            } else if (shouldUpdateInputManagement)
            {
                updateInputManagement();
            }
        }
    }

    public void setTarget(Vector3 target)
    {
        if (lastTarget.HasValue && target == lastTarget)
        {
            return;
        }
        lastTarget = target;
        path = new NavMeshPath();
        currentCornerIdx = 0;
        if (!agent.CalculatePath(target, path))
        {
            Debug.LogError("Failed to calculate path for AIController");
            return;
        }
        destinationReached = false;
        checkNextCorner(false);
        if (!destinationReached)
        {
            updateInputManagement();
        }
    }
    void updatePath()
    {
        if (path.status == NavMeshPathStatus.PathComplete || path.status == NavMeshPathStatus.PathPartial)
        {
            checkNextCorner(true);
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
        return transform.InverseTransformPoint(path.corners[currentCornerIdx]);
    }

    public override Vector3 getInput(float currentSpeed)
    {
        if (destinationReached)
        {
            return new Vector3(0, 0, 0);
        }
        Vector3 simulatedInput = getRelativeDirection().normalized;
        simulatedInput.y = 0;
        if (simulatedInput.z < 0) // we need to go backward
        {
            if (rotatingSide == DIRECTION.NONE)
            {
                rotatingSide = simulatedInput.x < 0 ? DIRECTION.LEFT : DIRECTION.RIGHT;
            }
            simulatedInput.x = rotatingSide == DIRECTION.RIGHT ? 1 : -1;
            simulatedInput.z = 0;
        }
        if (currentSpeed < minimalSpeed)
        {
            simulatedInput.z = 1f;
        }
        return simulatedInput.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        if (destinationReached)
        {
            return;
        }
        updatePath();
    }
}
