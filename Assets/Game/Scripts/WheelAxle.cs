using System;
using UnityEngine;

[Serializable]
public class WheelAxle
{
    [SerializeField] private WheelCollider leftWheelCollider;
    [SerializeField] private WheelCollider rightWheelCollider;

    [SerializeField] private Transform leftWheelMesh;
    [SerializeField] private Transform rightWheelMesh;

    [SerializeField] private bool isMotor;
    [SerializeField] private bool isSteer;

    //  Public API
    public void Update()
    {
        SyncMeshTransform();
    }

    public void ApplySteerAngle(float steerAngle)
    {
        if (isSteer == false) return;

        leftWheelCollider.steerAngle = steerAngle;
        rightWheelCollider.steerAngle = steerAngle;
    }

    public void ApplyMotorTorque(float motorTorque)
    {
        if (isMotor == false) return;

        leftWheelCollider.motorTorque = motorTorque;
        rightWheelCollider.motorTorque = motorTorque;
    }

    public void ApplyBrakeTorque(float brakeTorque)
    {
        leftWheelCollider.brakeTorque = brakeTorque;
        rightWheelCollider.brakeTorque = brakeTorque;
    }

    // Private
    private void SyncMeshTransform()
    {
        UpdateWheelTransform(leftWheelCollider, leftWheelMesh);
        UpdateWheelTransform(rightWheelCollider, rightWheelMesh);
    }

    private void UpdateWheelTransform(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 position;
        Quaternion rotation;
        wheelCollider.GetWorldPose(out position, out rotation);

        wheelTransform.position = position;
        wheelTransform.rotation = rotation;
    }
}
