using System;
using UnityEngine;

public class AIVision : MonoBehaviour
{
    public bool debug = true;
    public float visionRange = 10f;
    public Vector3? lastKnownPlayerPos { get; private set; } = null;
    public Vector3? lastKnownVelocity { get; private set; } = null;
    public Vector3? lastKnownPlayerForward { get; private set; } = null;
    public Vector3? lastKnownPlayerRight { get; private set; } = null;
    public float timeSinceLastSeen { get; private set;  } = 0f;
    public bool seePlayer = false;
    public LayerMask visionLayer = 1;

    public Transform viewPoint = null;
    private GameObject player;
    private Rigidbody playerRigidbody;
    private float squareVisionRange = 0f;
    // Start is called before the first frame update
    void Start()
    {
        squareVisionRange = visionRange * visionRange;
        player = GameObject.FindGameObjectWithTag("Player");
        playerRigidbody = player.GetComponent<Rigidbody>();
    }

    public Vector3? estimatedPlayerPosIn(float time)
    {
        if (!lastKnownPlayerPos.HasValue) return null;
        return lastKnownPlayerPos.Value + lastKnownVelocity.Value * time;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastSeen += Time.deltaTime;
        Vector3 diff = player.transform.position - viewPoint.position;
        // diff.y = player.transform.position.y;
        if (diff.sqrMagnitude < squareVisionRange)
        {
            RaycastHit hit;
            if (debug) Debug.DrawRay(viewPoint.position, diff, Color.red, Time.deltaTime);
            if (Physics.Raycast(viewPoint.position, diff, out hit, visionRange, visionLayer)
                && hit.transform.gameObject.GetComponentInParent<Player>())
            {
                lastKnownPlayerPos = player.transform.position;
                lastKnownPlayerForward = player.transform.forward;
                lastKnownVelocity = playerRigidbody.velocity;
                lastKnownPlayerRight = player.transform.right;
                timeSinceLastSeen = 0f;
                visionRange = 60f;
                seePlayer = true;
            } else {
            seePlayer = false;
            // if (debug && hit.collider) Debug.Log($"{hit.collider.gameObject.name}");
            }
                
        }
    }

    private void OnDrawGizmos() {
        if (!debug)
            return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, visionRange);
        if (lastKnownPlayerPos.HasValue) {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(lastKnownPlayerPos.Value, 1);
        }
    }
}
