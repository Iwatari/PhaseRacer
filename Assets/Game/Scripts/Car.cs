using UnityEngine;

[RequireComponent (typeof(CarChassis))]
public class Car : MonoBehaviour
{

    [SerializeField] private float maxSteerAngle;
    [SerializeField] private float maxBrakeTorque;

    [Header("Engine")]
    [SerializeField] private AnimationCurve engineTorqueCurve;
    [SerializeField] private float engineMaxTorque;
    // Debug
    [SerializeField] private float engineTorque;
    // Debug
    [SerializeField] private float engineRpm;

    [SerializeField] private float engineMinRpm;
    [SerializeField] private float engineMaxRpm;

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

        UpdateEngineTorque();

        if (LinearVelocity >= maxSpeed)
            engineTorque = 0;

        chassis.motorTorque = engineTorque * throttleControl;
        chassis.steerAngle = maxSteerAngle * steerControl;
        chassis.brakeTorque = maxBrakeTorque * brakeControl;
    }
    private void UpdateEngineTorque()
    {
        engineRpm = engineMinRpm + Mathf.Abs(chassis.GetAverageRpm() * 3.7f);
        engineRpm = Mathf.Clamp(engineRpm,engineMinRpm, engineMaxRpm);

        engineTorque = engineTorqueCurve.Evaluate(engineRpm / engineMaxRpm) * engineMaxTorque;
    }
}
