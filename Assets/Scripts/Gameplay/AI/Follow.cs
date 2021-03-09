using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Follow : MonoBehaviour
{
    public AIController controller;
    public GameObject target;
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
        if (updateTime <= 0f)
        {
            controller.setTarget(target.transform.position);
            updateTime = updateTimeout;
        }
    }
}
