using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FlockAgent : MonoBehaviour
{
    [Header("Agent Settings")]
    public bool debug = false;
    [Range(1f, 100f)]
    [SerializeField] protected float neighborRadius = 5f;
    [Range(0f, 1f)]
    [SerializeField] protected float avoidanceRadiusMultiplier = 0.5f;
    [Range(1f, 100f)]
    [SerializeField] protected float driveFactor = 10f;
    public float DriveFactor { get { return driveFactor; } }
    [SerializeField] protected LayerMask collisionMask = 1;

    //Computation optimisation
    protected float squareNeighborRadius;
    public float SquareNeighborRadius { get { return squareNeighborRadius; } }
    protected float squareAvoidanceRadius;
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }
    protected float avoidanceRadius;
    public float AvoidanceRadius { get { return avoidanceRadius; } }

    [Header("References")]
    [SerializeField] public Flock flock;
    [SerializeField] protected Rigidbody rb;

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        if (!rb)
            rb = GetComponent<Rigidbody>();
        squareNeighborRadius = neighborRadius * neighborRadius;
        avoidanceRadius = neighborRadius * avoidanceRadiusMultiplier;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;
    }

    public abstract void Move(Vector3 velocity);

    public List<Transform> GetNearbyObstacles() 
    {
        Collider[] obstacles = Physics.OverlapSphere(transform.position, neighborRadius, collisionMask);
        List<Transform> result = new List<Transform>();

        foreach (Collider obstacle in obstacles)
        {
            if (obstacle.attachedRigidbody != rb) {
                result.Add(obstacle.transform);
            }
        }
        return result;
    }
    private void OnDrawGizmos() {
        if (!debug)
            return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, neighborRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, neighborRadius * avoidanceRadiusMultiplier);
    }
}
