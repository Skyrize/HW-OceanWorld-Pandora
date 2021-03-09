using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Follow : MonoBehaviour
{
    public AIController controller;
    public AIVision vision;

    public float updateTimeout = 0.1f;

    private float updateTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        updateTime -= Time.deltaTime;
        if (updateTime <= 0f && vision.lastKnownPlayerPos.HasValue)
        {
            controller.setTarget(vision.lastKnownPlayerPos.Value);
            updateTime = updateTimeout;
        }
    }
}
