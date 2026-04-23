using UnityEngine;

[RequireComponent (typeof(CarChassis))]
public class Car : MonoBehaviour
{

    [SerializeField] private float maxSteerAngle;
    [SerializeField] private float maxBrakeTorque;

    [SerializeField] private AnimationCurve engineTorqueCurve;
    [SerializeField] private float maxMotorTorque;
    [SerializeField] private float maxSpeed;

    public float LinearVelocity => chassis.LinearVelocity;
    public float WheelSpeed => chassis.GetWheelSpeed();
    public float MaxSpeed => maxSpeed;

    private CarChassis chassis;

    // Debug
    [SerializeField] public float linearVelocity;
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
        linearVelocity = LinearVelocity;
        float engineTorque = engineTorqueCurve.Evaluate(LinearVelocity / maxSpeed) * maxMotorTorque;

        if (LinearVelocity >= maxSpeed)
            engineTorque = 0;

        chassis.motorTorque = engineTorque * throttleControl;
        chassis.steerAngle = maxSteerAngle * steerControl;
        chassis.brakeTorque = maxBrakeTorque * brakeControl;
    }
}
