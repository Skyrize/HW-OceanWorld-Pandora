using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avoidance : MonoBehaviour
{
    [Header("Settings")]
    [Range(1f, 100f)]
    [SerializeField] protected float avoidanceRadius = 5f;
    [SerializeField] protected LayerMask collisionMask = 1;
    [SerializeField] protected bool debug = false;

    // [SerializeField] protected float protectionWidth = 0.2f;
    // [SerializeField] protected float protectionLength = 100f;
    // [SerializeField] protected float protectionAngle = 2f;
    // [SerializeField] protected int nbProtectionRays = 3;
    // [SerializeField] protected float debugProtectionMultiplier = 1f;

    //Computation optimisation

    protected Rigidbody rb;

    private List<Vector3> debugPoints = new List<Vector3>();
    // public Vector3 CalculateMove()
    // {
    //     Vector3 avoidanceMove = Vector3.zero;
    //     Physics.Spe
    //     float length = protectionLength * debugProtectionMultiplier;
    //     Gizmos.color = Color.green;
    //     Gizmos.DrawLine(transform.position, transform.position + transform.forward * length);

    //     Gizmos.color = Color.red;
    //     float width = protectionWidth / (nbProtectionRays + 1);
    //     for (int i = 0; i != nbProtectionRays; i++) {
    //         float angleMult = (float)(i + 1) / (float)nbProtectionRays;
    //         Vector3 pointLeft = transform.position - transform.right * (width * (float)(i + 1));
    //         Vector3 pointRight = transform.position + transform.right * (width * (float)(i + 1));
    //         Vector3 rayLeft = Quaternion.AngleAxis(-protectionAngle * angleMult, Vector3.up) * transform.forward * length;
    //         Vector3 rayRight = Quaternion.AngleAxis(protectionAngle * angleMult, Vector3.up) * transform.forward * length;

    //         Gizmos.DrawRay(pointLeft, rayLeft);
    //         Gizmos.DrawRay(pointRight, rayRight);
    //     }

    //     return avoidanceMove;
    // }

    public Vector3 CalculateMove()
    {
        List<Transform> context = GetNearbyObstacles();
        Vector3 avoidanceMove = Vector3.zero;
        int avoidCount = 0;
        // Vector3 front = rb.velocity.normalized;

        debugPoints.Clear();
        // if (debug) Debug.DrawRay(transform.position, front * 5f, Color.cyan, Time.deltaTime);
        foreach (Transform item in context)
        {
            Vector3 closestPoint = item.GetComponent<Collider>().ClosestPoint(transform.position);
            Vector3 dist = transform.position - closestPoint;
            Vector3 norm = dist.normalized;
            // Debug.DrawRay(transform.position, -norm * 5f, Color.magenta, Time.deltaTime);
            float dot = (Vector3.Dot(transform.forward, -norm));
            dot = (Mathf.Clamp(dot, 0.01f, 1));
            debugPoints.Add(closestPoint);
            avoidCount++;
            avoidanceMove += (norm * avoidanceRadius - dist) * dot;
            if (debug) Debug.DrawRay(transform.position, (norm * avoidanceRadius - dist), Color.white, Time.deltaTime);
        }
        if (avoidCount != 0) {
            avoidanceMove /= avoidCount;
        }
        avoidanceMove.y = 0;

        if (debug) Debug.DrawRay(transform.position, avoidanceMove, Color.yellow, Time.deltaTime);
        return transform.InverseTransformDirection(avoidanceMove);
    }
    // Start is called before the first frame update
    void Start()
    {
        if (!rb)
            rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public List<Transform> GetNearbyObstacles()
    {
        Collider[] obstacles = Physics.OverlapSphere(transform.position, avoidanceRadius, collisionMask); // TODO : optimize (regular collider or refresh every x time)
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

        // float length = protectionLength * debugProtectionMultiplier;
        // Gizmos.color = Color.green;
        // Gizmos.DrawLine(transform.position, transform.position + transform.forward * length);

        // Gizmos.color = Color.red;
        // float width = protectionWidth / (nbProtectionRays + 1);
        // for (int i = 0; i != nbProtectionRays; i++) {
        //     float angleMult = (float)(i + 1) / (float)nbProtectionRays;
        //     Vector3 pointLeft = transform.position - transform.right * (width * (float)(i + 1));
        //     Vector3 pointRight = transform.position + transform.right * (width * (float)(i + 1));
        //     Vector3 rayLeft = Quaternion.AngleAxis(-protectionAngle * angleMult, Vector3.up) * transform.forward * length;
        //     Vector3 rayRight = Quaternion.AngleAxis(protectionAngle * angleMult, Vector3.up) * transform.forward * length;

        //     Gizmos.DrawRay(pointLeft, rayLeft);
        //     Gizmos.DrawRay(pointRight, rayRight);
        // }
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, avoidanceRadius);

        Gizmos.color = Color.magenta;
        foreach (var item in debugPoints)
        {
            Gizmos.DrawWireSphere(item, .5f);
        }


    }
}
