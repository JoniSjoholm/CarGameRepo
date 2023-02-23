using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControllerNew : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private float handBrakeInput;
    private float steerAngle;
    private float speed;
    private float slipAngle;
    private Rigidbody rb;

    public WheelCollider frontDriverW, frontPassengerW;
    public WheelCollider rearDriverW, rearPassengerW;
    public Transform frontDriverT, frontPassengerT;
    public Transform rearDriverT, rearPassengerT;
    public float brakeInput;
    public float maxSteerAngle = 30.0f;
    public float motorForce = 700f;
    public float handBrakeForce = 50000f;
    public float brakeForce = 50000f;
    public AnimationCurve steeringCurve;

    [SerializeField] private Vector3 _centerOfMass;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = _centerOfMass;
    }

    public void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        handBrakeInput = Input.GetAxis("Jump");
        slipAngle = Vector3.Angle(transform.forward, rb.velocity - transform.forward);
        float movingDirection = Vector3.Dot(transform.forward, rb.velocity);

        if (movingDirection < -0.5f && verticalInput > 0)
        {
            brakeInput = Mathf.Abs(verticalInput);
        }
        else if (movingDirection > 0.5f && verticalInput < 0)
        {
            brakeInput = Mathf.Abs(verticalInput);
        }
        else
        {
            brakeInput = 0;
        }
        
    }

    public void Steer()
    {
        steerAngle = horizontalInput * steeringCurve.Evaluate(speed);
        if (slipAngle < 120f)
        {
            steerAngle += Vector3.SignedAngle(transform.forward, rb.velocity + transform.forward, Vector3.up);
        }
        steerAngle = Mathf.Clamp(steerAngle, -90f, 90f);
        frontDriverW.steerAngle = Mathf.Lerp(frontDriverW.steerAngle, steerAngle, 0.4f); 
        frontPassengerW.steerAngle = Mathf.Lerp(frontPassengerW.steerAngle, steerAngle, 0.4f);
    }

    public void Accelerate()
    {
        frontDriverW.motorTorque = verticalInput * motorForce * 0.65f;
        frontPassengerW.motorTorque = verticalInput * motorForce * 0.65f;
        rearDriverW.motorTorque = verticalInput * motorForce;
        rearPassengerW.motorTorque = verticalInput * motorForce;
    }

    public void Brake()
    {
        frontDriverW.brakeTorque = brakeInput * brakeForce * 0.75f;
        frontPassengerW.brakeTorque = brakeInput * brakeForce * 0.75f;
        rearDriverW.brakeTorque = brakeInput * brakeForce * 0.3f;
        rearPassengerW.brakeTorque = brakeInput * brakeForce * 0.3f;
    }

    public void Handbrake()
    {
        rearDriverW.brakeTorque = handBrakeInput * handBrakeForce;
        rearPassengerW.brakeTorque = handBrakeInput * handBrakeForce;
        rearDriverW.motorTorque = 0;
        rearPassengerW.motorTorque = 0;
    }
    private void UpdateWheelPoses()
    {
        UpdateWheelPose(frontDriverW, frontDriverT);
        UpdateWheelPose(frontPassengerW, frontPassengerT);
        UpdateWheelPose(rearDriverW, rearDriverT);
        UpdateWheelPose(rearPassengerW, rearPassengerT);
    }

    private void UpdateWheelPose(WheelCollider _collider, Transform _transform)
    {
        Vector3 _pos = _transform.position;
        Quaternion _quat = _transform.rotation;

        _collider.GetWorldPose(out _pos, out _quat);
        _transform.position = _pos;
        _transform.rotation = _quat;
    }
    private void Update()
    {
        GetInput();
        speed = rb.velocity.magnitude;
    }

    private void FixedUpdate()
    {
        Steer();
        Accelerate();
        Brake();
        Handbrake();
        UpdateWheelPoses();
    }


}
