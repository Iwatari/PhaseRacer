using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof(CarChassis))]
public class Car : MonoBehaviour
{
    public event UnityAction<string> GearChanged;
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

    [Header("Gearbox")]
    [SerializeField] private float[] gears;
    [SerializeField] private float finalDriveRatio;

    //Debug
    [SerializeField] private int selectedGearIndex;
    [SerializeField] private float selectedGear;
    [SerializeField] private float rearGear;
    [SerializeField] private float upShiftEngineRpm;
    [SerializeField] private float downShiftEngineRpm;

    [SerializeField] private float maxSpeed;

    public float LinearVelocity => chassis.LinearVelocity;
    public float WheelSpeed => chassis.GetWheelSpeed();
    public float MaxSpeed => maxSpeed;

    public float EngineRpm => engineRpm;
    public float EngineMaxRpm => engineMaxRpm;

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
    public string GetSelectedGearName()
    {
        if (selectedGear == rearGear) return "R";

        if (selectedGear == 0) return "N";

        return (selectedGearIndex + 1).ToString();
    }

    private void Update()
    {
        linearVelocity = LinearVelocity;

        UpdateEngineTorque();

        AutoGearShift();

        if (LinearVelocity >= maxSpeed)
            engineTorque = 0;

        chassis.motorTorque = engineTorque * throttleControl;
        chassis.steerAngle = maxSteerAngle * steerControl;
        chassis.brakeTorque = maxBrakeTorque * brakeControl;
    }
    //Gearbox
    private void AutoGearShift()
    {
        if (selectedGear < 0) return;

        if (engineRpm >= upShiftEngineRpm)
            UpGear();

        if (engineRpm < downShiftEngineRpm)
            DownGear();

        selectedGearIndex = Mathf.Clamp(selectedGearIndex, 0, gears.Length - 1);
    }
    public void UpGear()
    {
        ShiftGear(selectedGearIndex + 1);
    }

    public void DownGear()
    {
        ShiftGear(selectedGearIndex - 1);
    }

    public void ShiftToReverseGear()
    {
        selectedGear = rearGear;
        GearChanged?.Invoke(GetSelectedGearName());
    }

    public void ShiftToFirstGear()
    {
        ShiftGear(0);
    }

    public void ShiftToNetral()
    {
        selectedGear = 0;
        GearChanged?.Invoke(GetSelectedGearName());
    }

    private void ShiftGear(int gearIndex)
    {
        gearIndex = Mathf.Clamp(gearIndex, 0, gears.Length - 1);
        selectedGear = gears[gearIndex];
        selectedGearIndex = gearIndex;
        GearChanged?.Invoke(GetSelectedGearName());
    }
    private void UpdateEngineTorque()
    {
        engineRpm = engineMinRpm + Mathf.Abs(chassis.GetAverageRpm() * selectedGear * finalDriveRatio);
        engineRpm = Mathf.Clamp(engineRpm,engineMinRpm, engineMaxRpm);

        engineTorque = engineTorqueCurve.Evaluate(engineRpm / engineMaxRpm) * engineMaxTorque * finalDriveRatio * Mathf.Sign(selectedGear) * gears[0];
    }
}
