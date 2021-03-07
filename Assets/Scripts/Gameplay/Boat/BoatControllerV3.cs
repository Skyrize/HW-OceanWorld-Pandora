using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BoatControllerV3 : MonoBehaviour
{
    public Controller controller;
    public NavMeshAgent agent;
    protected float currentSpeed = 0;
    
    private void Start()
    {
        agent.updateRotation = false;
    }

    void speedControl(float speedInput)
    {
        if (speedInput == 0 && agent.autoBraking)
        {
            speedInput = -1;
        }
        currentSpeed += agent.acceleration * speedInput * Time.deltaTime;
        currentSpeed = Mathf.Clamp(currentSpeed, 0, agent.speed);
    }

    float getFrameRotation(float rotateInput)
    {
        float rotate = rotateInput * agent.angularSpeed * Time.deltaTime;
        return rotate;
    }

    void Update()
    {
        Vector3 input = controller.getInput(currentSpeed).normalized;
        speedControl(input.z);
        float rotation = getFrameRotation(input.x);
        Vector3 directionVec = Quaternion.AngleAxis(rotation, Vector3.up) * transform.forward;
        transform.Rotate(new Vector3(0, rotation, 0));
        agent.velocity = currentSpeed * directionVec;
    }
}
