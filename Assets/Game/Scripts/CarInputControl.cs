using System;
using UnityEngine;

public class CarInputControl : MonoBehaviour
{
    [SerializeField] private Car car;
    [SerializeField] private AnimationCurve breakCurve;
    [SerializeField] private AnimationCurve steerCurve;
    [SerializeField][Range(0.0f, 1.0f)] private float autoBreakStrength = 0.5f;

    private float wheelSpeed;
    private float verticalAxis;
    private float horizontalAxis;
    private float handBreakAxis;
    private void Update()
    {
        wheelSpeed = car.WheelSpeed;

        UpdateAxis();

        UpdateThrottleBreak();
        UpdateSteer();

        UpdateAutoBreak();
    }

    private void UpdateThrottleBreak()
    {
        // проверяем двигаются ли колёса в ту же сторону куда двигается машина
        if (Mathf.Sign(verticalAxis) == Mathf.Sign(wheelSpeed) || Mathf.Abs(wheelSpeed) < 0.5f)
        {
            car.throttleControl = verticalAxis;
            car.brakeControl = 0;
        }
        else
        {
            car.throttleControl = 0;
            car.brakeControl = breakCurve.Evaluate(car.WheelSpeed / car.MaxSpeed);
        }
    }
    private void UpdateSteer()
    {
        car.steerControl = steerCurve.Evaluate(car.WheelSpeed / car.MaxSpeed) * horizontalAxis;
    }

    private void UpdateAutoBreak()
    {
        if (verticalAxis == 0)
            car.brakeControl = breakCurve.Evaluate(wheelSpeed / car.MaxSpeed) * autoBreakStrength;
    }


    private void UpdateAxis()
    {
        verticalAxis = Input.GetAxis("Vertical");
        horizontalAxis = Input.GetAxis("Horizontal");
        handBreakAxis = Input.GetAxis("Jump");
    }
}
