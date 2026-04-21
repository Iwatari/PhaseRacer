using UnityEngine;

[RequireComponent (typeof(CarChassis))]
public class Car : MonoBehaviour
{

    [SerializeField] private float maxMotorTorque;
    [SerializeField] private float maxSteerAngle;
    [SerializeField] private float maxBrakeTorque;

    private CarChassis chassis;

    // Debug
    public float throttleControl;
    public float steerControl;
    public float brakeControl;
    public float handBrakeControl;

    private void Start()
    {
        chassis = GetComponent<CarChassis>();
    }

    private void Update()
    {
        chassis.motorTorque = maxMotorTorque * throttleControl;
        chassis.steerAngle = maxSteerAngle * steerControl;
        chassis.brakeTorque = maxBrakeTorque * brakeControl;
    }
}
