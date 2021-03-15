using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIVision : MonoBehaviour
{
    public float visionRange = 10f;
    public Vector3? lastKnownPlayerPos { get; private set; } = null;
    public Vector3? lastKnownVelocity { get; private set; } = null;
    public Vector3? lastKnownPlayerForward { get; private set; } = null;
    public float timeSinceLastSeen { get; private set;  } = 0f;

    private GameObject player;
    private Rigidbody playerRigidbody;
    // Start is called before the first frame update
    void Start()
    {
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
        if (diff.magnitude < visionRange)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, diff.normalized, out hit, visionRange)
                && hit.transform.gameObject.CompareTag("Player") 
            )
            {
                lastKnownPlayerPos = player.transform.position;
                lastKnownPlayerForward = player.transform.forward;
                lastKnownVelocity = playerRigidbody.velocity;
                timeSinceLastSeen = 0f;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0.5f, 0f);
        Gizmos.DrawWireSphere(transform.position, visionRange);
    }
}
