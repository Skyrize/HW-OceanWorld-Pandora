using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum TMPFireSide
{
    NONE,
    LEFT,
    RIGHT,
    BOTH
}

public class AIFleetController : FleetController
{
    [Header("Settings")]
    [SerializeField] protected float arrivalDist = 35f;
    public float ArrivalDist { get { return arrivalDist; } }
    [SerializeField] protected float detectionRange = 200f, attackRange = 100f, chaseRatio = .33f;
    [SerializeField] protected int fireRatio = 2;
    [SerializeField] protected bool debug = false;

    [Header("References")]
    [SerializeField]
    protected WaypointManager waypoints = null;
    [SerializeField]
    protected AIVision vision = null;
    [Header("Runtime")]
    [SerializeField]
    protected Player currentTarget = null;
    [SerializeField]
    public Transform CurrentWaypoint = null;

    private float squareAttackRange = 0;

    override protected void Start() {
        base.Start();
        squareAttackRange = attackRange * attackRange;
    }

    public void UpdateWaypoint()
    {
        if (!CurrentWaypoint || Vector3.Distance(Position, CurrentWaypoint.position) < arrivalDist) {
            CurrentWaypoint = waypoints.GetRandomWaypoint();
        }

        SetDestination(CurrentWaypoint.position);
    }

    public bool DetectTarget()
    {
        bool result = false;
        
        currentTarget = null;
        // foreach (FleetController fleet in playerFleets)
        // {
        //     if (fleet.RemainingBoats != 0) {
        //         if (!currentTarget) {
        //             if (Vector3.Distance(fleet.Position, Position) <= detectionRange) {
        //                 result = true;
        //                 currentTarget = fleet;
        //             }
        //         } else {
        //             if (Vector3.Distance(fleet.Position, Position) < Vector3.Distance(currentTarget.Position, Position)) {
        //                 result = true;
        //                 currentTarget = fleet;
        //             }

        //         }
        //     }
        // }
        return result;
    }

    public void ChaseTarget()
    {
        Transform target = currentTarget.transform.GetChild(0);
        Transform captain = transform.GetChild(0);
        float dot = Mathf.Abs(Vector3.Dot(target.forward, captain.forward));
        Vector3 leftPoint = target.position + (- target.right + Vector3.Lerp(target.forward, captain.forward, dot)) * attackRange * chaseRatio;
        Vector3 rightPoint = target.position + (target.right + Vector3.Lerp(target.forward, captain.forward, dot)) * attackRange * chaseRatio;

        if (captain.InverseTransformPoint(target.position).x > 0) {
            SetDestination(leftPoint);
        } else {
            SetDestination(rightPoint);
        }
    }

    public bool IsInAttackRange()
    {
        return (currentTarget.transform.position - Position).sqrMagnitude <= squareAttackRange;
    }

    public TMPFireSide ShouldFire(Transform boat)
    {
        TMPFireSide result = TMPFireSide.NONE;
        // RaycastHit hit;
        // Vector3 leftDirection = - boat.right * attackRange;
        // Vector3 rightDirection = boat.right * attackRange;

        // if (Physics.Raycast(boat.position, leftDirection, out hit, attackRange)) {
        //     BoatAgent target = hit.transform.gameObject.GetComponent<BoatAgent>();
        //     if (target && target.Team == Team.PLAYER) {
        //         result = TMPFireSide.LEFT;
        //     }
        //     if (debug)
        //         Debug.DrawLine(boat.position, hit.point, Color.green, Time.deltaTime);
        // } else {
        //     if (debug)
        //         Debug.DrawRay(boat.position, leftDirection * attackRange, Color.red, Time.deltaTime);
        // }
        // if (Physics.Raycast(boat.position, rightDirection, out hit, attackRange)) {
        //     BoatAgent target = hit.transform.gameObject.GetComponent<BoatAgent>();
        //     if (target && target.Team == Team.PLAYER) {
        //         if (result == TMPFireSide.NONE) {
        //             result = TMPFireSide.RIGHT;
        //         } else {
        //             result = TMPFireSide.BOTH;
        //         }
        //     }
        //     if (debug)
        //         Debug.DrawLine(boat.position, hit.point, Color.green, Time.deltaTime);
        // } else {
        //     if (debug)
        //         Debug.DrawRay(boat.position, rightDirection * attackRange, Color.red, Time.deltaTime);
        // }
        return result;
    }

    public TMPFireSide ShouldFire()
    {
        TMPFireSide result = TMPFireSide.NONE;

        //TODO : CHECK 

        // int nbBoat = transform.childCount - 1;
        // int nbLeft = 0;
        // int nbRight = 0;
        // Transform currentBoat;
        // int minShotSelf = (int)Mathf.Max(Mathf.Ceil((float)nbBoat / (float)fireRatio), 1);
        // int minShotTarget = (int)Mathf.Max(Mathf.Ceil((float)currentTarget.RemainingBoats / (float)fireRatio), 1);
        // int minShot = Mathf.Min(minShotSelf, minShotTarget);

        // for (int i = 0; i != nbBoat; i++) {
        //     currentBoat = transform.GetChild(i);
        //     TMPFireSide boatTMPFireSide = ShouldFire(currentBoat);
            
        //     switch (boatTMPFireSide)
        //     {
        //         case TMPFireSide.NONE:
        //         break;
        //         case TMPFireSide.LEFT:
        //         nbLeft++;
        //         break;
        //         case TMPFireSide.RIGHT:
        //         nbRight++;
        //         break;
        //         case TMPFireSide.BOTH:
        //         nbLeft++;
        //         nbRight++;
        //         break;
        //     }
        // }
        // if (nbLeft >= minShot) {
        //     result = TMPFireSide.LEFT;
        // }
        // if (nbRight >= minShot) {
        //     if (result == TMPFireSide.NONE) {
        //         result = TMPFireSide.RIGHT;
        //     } else {
        //         result = TMPFireSide.BOTH;
        //     }
        // }
        return result;
    }

    public void Fire(TMPFireSide side)
    {
        switch (side)
        {
            case TMPFireSide.RIGHT:
            FireRight();
            break;
            case TMPFireSide.LEFT:
            FireLeft();
            break;
            case TMPFireSide.BOTH:
            FireRight();
            FireLeft();
            break;
        }
    }

    private void OnDrawGizmos() {
        if (!debug)
            return;
        Transform boat = transform.GetChild(0);
        if (boat) {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(boat.position, arrivalDist);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(boat.position, detectionRange);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(boat.position, attackRange);
        }
    }
}
