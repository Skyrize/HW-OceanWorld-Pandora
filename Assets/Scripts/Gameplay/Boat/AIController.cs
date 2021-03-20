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
    public bool debug = true;
    [SerializeField] protected float avoidWeight = 1f;
    [SerializeField] protected float followPathWeight = 1f;
    public float minimalSpeed = 1f;
    public float minimalAcceleration = 0.3f;
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

    // private DIRECTION rotatingSide = DIRECTION.NONE;

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

    // void checkNextCorner(bool shouldUpdateInputManagement)
    // {
    //     // while ((path.corners[currentCornerIdx] - transform.position).magnitude < agent.stoppingDistance)
    //     // {
    //     //     currentCornerIdx += 1;
    //     //     if (currentCornerIdx >= path.corners.Length)
    //     //     {
    //     //         destinationReached = true;
    //     //         return;
    //     //     } else if (shouldUpdateInputManagement)
    //     //     {
    //     //         updateInputManagement();
    //     //     }
    //     // }
    // }

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
            if (debug) Debug.LogError("Failed to calculate path for AIController");
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

    // void updateInputManagement()
    // {
    //     bool needToGoBackward = getRelativeDirection().z < 0f;
    //     if (!needToGoBackward)
    //     {
    // //         rotatingSide = DIRECTION.NONE;
    //     }
    // }

    private Vector3 getRelativeDirection()
    {
        if (path.corners.Length == 0)
            return Vector3.forward;
        Vector3 direction = transform.InverseTransformPoint(path.corners[1]);
        direction.y = 0;
        direction.Normalize();

        if (direction.z < 0) {
            direction.x = 1 * Mathf.Sign(direction.x);
        }
        direction.z = Mathf.Max(direction.z, minimalAcceleration);
        return direction;
    }

    [SerializeField] private bool reverse = false;
    [SerializeField] private float reverseTime = 1f;

    WaitForSeconds timer = null;

    private void Start() {
        timer = new WaitForSeconds(reverseTime);
    }

    IEnumerator EndReverse()
    {
        yield return timer;
        reverse = false;
    }

    void CheckReverse()
    {
        if (avoid.IsBlocked() && !reverse) {
            reverse = true;
            StartCoroutine(EndReverse());
        }
    }

    public override Vector3 getInput()
    {
        CheckReverse();
        if (reverse) {
            return avoid.GetReverseDirection();
        }
        if (destinationReached)
        {
            return new Vector3(0, 0, 0);
        }
        Vector3 avoidanceInput = avoid.CalculateMove() * avoidWeight;
        // Vector3 avoidanceInput = (avoid.CalculateMove().normalized + Vector3.forward) * avoidWeight;
        Vector3 simulatedInput = getRelativeDirection().normalized * followPathWeight;
        // float dot = Vector3.Dot(avoidanceInput, simulatedInput);
        // Vector3 result = dot <= -0.99f ? simulatedInput : simulatedInput + avoidanceInput;
        Vector3 result = simulatedInput + avoidanceInput;
        result.y = 0;
        result.Normalize();


        if (debug) Debug.DrawRay(transform.position, transform.TransformDirection(simulatedInput) * 10, Color.cyan, Time.deltaTime);
        if (debug) Debug.DrawRay(transform.position + Vector3.up * 2, transform.TransformDirection(avoidanceInput), Color.magenta, Time.deltaTime);
        // result.x = Mathf.Clamp(result.x, -1, 1);
        // result.z = Mathf.Clamp(result.z, -1, 1);

        // if (Mathf.Abs(result.x) <= 0.05f) result.x = avoidanceInput.x;
        // if (Mathf.Abs(result.z) <= 0.05f && rb.velocity.sqrMagnitude < squareMinimalSpeed) {
        //     result.z = 1f;
        //     Debug.Log("--------------------------------------------------------------------------- yup");
        // }
            // Debug.Log($"final {result.ToString()}");

        if (debug) Debug.DrawRay(transform.position + Vector3.up * 4, transform.TransformDirection(result) * 15, Color.black, Time.deltaTime);
        // return Vector3.zero;
        return result.normalized;
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
