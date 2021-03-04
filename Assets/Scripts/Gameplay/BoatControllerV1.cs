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
    // [SerializeField] protected  float motorRPM = 0;
    // [SerializeField] protected  float maxMotorRPM = 10;

    [Header("References")]
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected Transform motor;

    [Header("Runtime")]
    [SerializeField] protected float accelerationInput = 0;
    [SerializeField] protected float steerInput = 0;

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
        accelerationInput = Input.GetAxis("Vertical");
        // accelerationInput = Mathf.Clamp(accelerationInput, 0, maxaccelerationInput);
        steerInput = Input.GetAxisRaw("Horizontal");
    }

    void Move()
    {
        Vector3 accelerationForce = motor.forward * accelerationInput * acceleration;

        rb.AddForceAtPosition(accelerationForce, motor.position, ForceMode.Force);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
    }

    void Steer()
    {
        Quaternion targetAngle = Quaternion.Euler(0, maxSteerAngle * -steerInput, 0);
        motor.localRotation = Quaternion.Slerp(motor.localRotation, targetAngle, Time.deltaTime * steerSpeed);
    }

    private void FixedUpdate() {
        
        // Steer Force
        Move();
        Steer();
    }
}
