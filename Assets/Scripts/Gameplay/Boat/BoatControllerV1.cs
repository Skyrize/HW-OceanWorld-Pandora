using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatControllerV1 : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] protected  float acceleration = 1;
    [SerializeField] protected  float maxSpeed = 10;
    [SerializeField] protected  float steerSpeed = 1;
    [SerializeField] protected  float maxSteerAngle = 10;

    [SerializeField] protected  float slideForce = 1;
    // [SerializeField] protected  float motorRPM = 0;
    // [SerializeField] protected  float maxMotorRPM = 10;

    [Header("References")]
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected Transform motor;

    [Header("Runtime")]
    [SerializeField] protected Vector2 input = Vector2.zero;

    protected Quaternion baseRotation;

    // Start is called before the first frame update
    void Start()
    {
        if (!rb)
            rb = GetComponent<Rigidbody>();
        baseRotation = motor.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        input.y = Input.GetAxis("Vertical");
        input.x = Input.GetAxisRaw("Horizontal");
        input.Normalize();
    }

    void Move()
    {
        Vector3 accelerationForce = motor.forward * input.y * acceleration;

        rb.AddForceAtPosition(accelerationForce, motor.position, ForceMode.Force);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
    }

    void Steer()
    {
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

    private void FixedUpdate() {
        
        // Steer Force
        Steer();
        Move();
        Slide();
    }
}
