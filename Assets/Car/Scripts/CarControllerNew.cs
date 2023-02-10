using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControllerNew : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private float steerAngle;
    private Rigidbody rb;

    public WheelCollider frontDriverW, frontPassengerW;
    public WheelCollider rearDriverW, rearPassengerW;
    public Transform frontDriverT, frontPassengerT;
    public Transform rearDriverT, rearPassengerT;
    public float maxSteerAngle = 30.0f;
    public float motorForce = 700f;

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
    }

    public void Steer()
    {
        steerAngle = maxSteerAngle * horizontalInput;
        frontDriverW.steerAngle = Mathf.Lerp(frontDriverW.steerAngle, steerAngle, 0.5f); 
        frontPassengerW.steerAngle = Mathf.Lerp(frontPassengerW.steerAngle, steerAngle, 0.5f);
    }

    public void Accelerate()
    {
        frontDriverW.motorTorque = verticalInput * motorForce;
        frontPassengerW.motorTorque = verticalInput * motorForce;
        rearDriverW.motorTorque = verticalInput * motorForce * 0.6f;
        rearPassengerW.motorTorque = verticalInput * motorForce * 0.6f;
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
    }

    private void FixedUpdate()
    {
        Steer();
        Accelerate();
        UpdateWheelPoses();
    }


}
