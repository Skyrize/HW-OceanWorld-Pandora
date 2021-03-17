using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Transform[] waypoints = null;
    [SerializeField] private Transform home = null;

    public Transform Home {
        get {
            return home;
        }
    }

    public Transform GetRandomWaypoint()
    {
        return waypoints[Random.Range(0, waypoints.Length)];
    }

    public Transform GetFarthestWaypoint(Vector3 location)
    {
        Transform result = waypoints[0];

        foreach (Transform waypoint in waypoints)
        {
            if (Vector3.Distance(waypoint.position, location) > Vector3.Distance(result.position, location)) {
                result = waypoint;
            }
        }
        return result;
    }
}
