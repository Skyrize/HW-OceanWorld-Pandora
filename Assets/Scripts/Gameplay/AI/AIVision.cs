using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIVision : MonoBehaviour
{
    public float visionRange = 10f;
    public Vector3? lastKnownPlayerPos { get; private set; } = null;
    public float timeSinceLastSeen { get; private set;  } = 0f;

    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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
                timeSinceLastSeen = 0f;
            }
        }
    }
}
