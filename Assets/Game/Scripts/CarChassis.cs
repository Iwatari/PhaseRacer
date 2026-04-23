using System;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent (typeof(Rigidbody))]
public class CarChassis : MonoBehaviour
{
    [SerializeField] private WheelAxle[] wheelAxles;
    [SerializeField] private float wheelBaseLength;

    [SerializeField] private Transform centerOfMass;

    [Header("DownForce")]
    [SerializeField] private float downForceMin;
    [SerializeField] private float downForceMax;
    [SerializeField] private float downForceFactor;

    [Header("AngularDrag")]
    [SerializeField] private float angularDragMin;
    [SerializeField] private float angularDragMax;
    [SerializeField] private float angularDragFactor;

    //DEBUG
    public float motorTorque;
    public float brakeTorque;
    public float steerAngle;

    public float LinearVelocity => rigidbody.linearVelocity.magnitude * 3.6f;

    private new Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        if (centerOfMass != null)
            rigidbody.centerOfMass = centerOfMass.localPosition;
    }
    private void FixedUpdate()
    {
        // стабильность (сопротивление вращению)
        UpdateAngularDrag();
        // прижимная сила (сопротивление воздуха)
        UpdateDownForce();

        UpdateWheelAxles();
    }

    public float GetAverageRpm()
    {
        float sum = 0;
        for (int i = 0; i < wheelAxles.Length; i++)
        {
            sum += wheelAxles[i].GetAverageRpm();
        }
        return sum / wheelAxles.Length;
    }
    public float GetWheelSpeed()
    {
        return GetAverageRpm() * wheelAxles[0].GetRadius() * 2 * 0.1885f;
    }

    private void UpdateAngularDrag()
    {
        rigidbody.angularDamping = Mathf.Clamp(angularDragFactor * LinearVelocity, angularDragMin, angularDragMax);
    }

    private void UpdateDownForce()
    {
        float downForce = Mathf.Clamp(downForceFactor * LinearVelocity, downForceMin, downForceMax);
        rigidbody.AddForce(-transform.up * downForce);
    }

    private void UpdateWheelAxles()
    {
        int amountMotorWheel = 0;

        for (int i = 0; i < wheelAxles.Length; i++)
        {
            if (wheelAxles[i].IsMotor == true)
                amountMotorWheel += 2;
        }

        for (int i = 0; i < wheelAxles.Length; i++)
        {
            wheelAxles[i].Update();

            wheelAxles[i].ApplyMotorTorque(motorTorque / amountMotorWheel);
            wheelAxles[i].ApplyBrakeTorque(brakeTorque);
            wheelAxles[i].ApplySteerAngle(steerAngle, wheelBaseLength);
        }
    }
}
