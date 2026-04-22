using System;
using UnityEngine;

public class CarChassis : MonoBehaviour
{
    [SerializeField] private WheelAxle[] wheelAxles;
    [SerializeField] private float wheelBaseLength;

    //DEBUG
    public float motorTorque;
    public float brakeTorque;
    public float steerAngle;

    private void FixedUpdate()
    {
        UpdateWheelAxles();
    }

    private void UpdateWheelAxles()
    {
        for (int i = 0; i < wheelAxles.Length; i++)
        {
            wheelAxles[i].Update();

            wheelAxles[i].ApplyMotorTorque(motorTorque);
            wheelAxles[i].ApplyBrakeTorque(brakeTorque);
            wheelAxles[i].ApplySteerAngle(steerAngle, wheelBaseLength);
        }
    }
}
