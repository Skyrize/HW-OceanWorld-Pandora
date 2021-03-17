using System.Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BoatAgent : FlockAgent
{
    [Header("Boat Settings")]
    [SerializeField] protected  float acceleration = 1;
    [Min(0)]
    [SerializeField] protected float maxSpeed = 5f;
    protected float squareMaxSpeed;
    [SerializeField] protected  float steerSpeed = 1;
    [SerializeField] protected  float maxSteerAngle = 10;
    [SerializeField] protected  float slideForce = 1;
    [SerializeField] protected float minAcceleration = 0.3f;


    [Header("Agent Controller")]
    [SerializeField] public Transform destination = null;
    protected NavMeshPath path;

    [Header("References")]
    [SerializeField] protected Transform motor;

    [Header("Runtime")]
    [SerializeField] protected Vector2 input = Vector2.zero;
    // [SerializeField] protected float targetSteerAngle = 5f;
    // [SerializeField] protected float currentSteerAngle = 0f;
    // [SerializeField] protected float steerInput = 0;
    // [SerializeField] protected float accelerationInput = 0;
    // [SerializeField] protected Vector3 direction = Vector3.zero;
    // [SerializeField] protected Vector3 finalForce = Vector3.zero;

    override protected void Awake() {
        base.Awake();
        squareMaxSpeed = maxSpeed * maxSpeed;
        path = new NavMeshPath();
    }

    public override void Move(Vector3 newDirection)
    {
        if (!isActiveAndEnabled) {
            rb.velocity = Vector3.zero;
            return;
        }
        newDirection = transform.InverseTransformDirection(newDirection).normalized;
        newDirection.y = 0;
        float dot = Vector3.Dot(newDirection, Vector3.forward);
        if (dot < 0) {
            newDirection.x = 1f * Mathf.Sign(newDirection.x);
        }
        newDirection.z = Mathf.Max(newDirection.z, minAcceleration);
        if (debug) {
            Debug.DrawLine(transform.position, transform.position + transform.TransformDirection(newDirection) * 10, Color.black, Time.fixedDeltaTime);
        }
        input.y = newDirection.z;
        input.x = newDirection.x;
        Move();
        Steer();
        Slide();
        // Float();
    }

    virtual protected Vector3 GetDirection()
    {   
        Vector3 direction = transform.InverseTransformPoint(path.corners[1]);
        direction.y = 0;
        direction.Normalize();

        return direction;
    }
    
    public Vector3 GetInput() {
        if (destination == null)
            return transform.forward;
        Vector3 direction = transform.forward;
        if (NavMesh.CalculatePath(transform.position, destination.position, NavMesh.AllAreas, path)) { // TODO: refresh only when near next point

            direction = GetDirection();

            if (debug) {
                for (int i = 0; i < path.corners.Length - 1; i++) {
                    Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red, Time.fixedDeltaTime);
                }
            }
        } else if (debug) Debug.Log("No path found");
        return transform.TransformDirection(direction); // normalize or not ?
    }

    void Move() {
        Vector3 accelerationForce = motor.forward * input.y * acceleration;

        rb.AddForceAtPosition(accelerationForce, motor.position, ForceMode.Force);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        if (debug) {
            Debug.DrawLine(transform.position, transform.position + rb.velocity * 10, Color.green, Time.fixedDeltaTime);
            Debug.DrawLine(motor.position, motor.position + accelerationForce * 10, Color.cyan, Time.fixedDeltaTime);
        }
    }

    void Steer() {
        Quaternion targetAngle = Quaternion.Euler(0, maxSteerAngle * -input.x, 0);
        float steerSpeed = targetAngle == Quaternion.identity ? Mathf.Pow(1 + this.steerSpeed, 2) : this.steerSpeed;

        motor.localRotation = Quaternion.Slerp(motor.localRotation, targetAngle, Time.fixedDeltaTime * steerSpeed);
    }

    void Slide()
    {
        Vector3 forwardVelocity = transform.forward * rb.velocity.magnitude;
        Vector3 slideVelocity = Vector3.Lerp(rb.velocity, forwardVelocity, Time.fixedDeltaTime * slideForce);

        rb.velocity = slideVelocity;
    }

    void Float()
    {
        rb.centerOfMass = transform.position;
        Vector3 rectify = transform.position;
        rectify.y = 0;
        transform.position = rectify;
    }
}
