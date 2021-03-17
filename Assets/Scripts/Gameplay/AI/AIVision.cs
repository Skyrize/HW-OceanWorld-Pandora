using System;
using System.Collections;
using System.Collections.Generic;
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
        Vector3 diff = player.gameObject.transform.position - transform.position;
        if (diff.sqrMagnitude < squareVisionRange)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, diff, out hit, visionRange)
                && hit.transform.gameObject.CompareTag("Player"))
            {
                lastKnownPlayerPos = player.transform.position;
                lastKnownPlayerForward = player.transform.forward;
                lastKnownVelocity = playerRigidbody.velocity;
                lastKnownPlayerRight = player.transform.right;
                timeSinceLastSeen = 0f;
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
