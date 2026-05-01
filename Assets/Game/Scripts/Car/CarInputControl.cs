using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInputControl : MonoBehaviour
{
    [SerializeField] private Car car;
    [SerializeField] private AnimationCurve breakCurve;
    [SerializeField] private AnimationCurve steerCurve;
    [SerializeField] private AudioSource audioTransmission;

    [SerializeField][Range(0.0f, 1.0f)] private float autoBreakStrength = 0.5f;

    private float wheelSpeed;
    private float verticalAxis;
    private float horizontalAxis;
    private float handbreakAxis;

    private void Update()
    {
        wheelSpeed = car.WheelSpeed;

        UpdateAxis();

        UpdateThrottleAndBreak();
        UpdateSteer();

        UpdateAutoBreak();

        if (Input.GetKeyDown(KeyCode.E))
        {
            audioTransmission.Play();
            car.upGear();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            audioTransmission.Play();
            car.DownGear();
        }
    }

    private void UpdateThrottleAndBreak()
    {
        // проверяем двигаются ли колёса в ту же сторону куда двигается машина
        if (Mathf.Sign(verticalAxis) == Mathf.Sign(wheelSpeed) || Mathf.Abs(wheelSpeed) < 0.5f)
        {
            car.ThrottleControl = Mathf.Abs(verticalAxis);
            car.BrakeControl = 0;
        }
        else
        {
            car.ThrottleControl = 0;
            car.BrakeControl = breakCurve.Evaluate(wheelSpeed / car.MaxSpeed);
        }

        //Gear
        if (verticalAxis < 0 && wheelSpeed > -0.5f && wheelSpeed <= 0.5f)
        {
            car.ShiftToReverseGear();
        }

        if (verticalAxis > 0 && wheelSpeed > -0.5f && wheelSpeed < 0.5f)
        {
            car.ShiftToFirstGear();
        }
    }

    private void UpdateSteer()
    {
        car.SteerControl = steerCurve.Evaluate(car.WheelSpeed / car.MaxSpeed) * horizontalAxis;
    }

    private void UpdateAutoBreak()
    {
        if (verticalAxis == 0)
        {
            car.BrakeControl = breakCurve.Evaluate(wheelSpeed / car.MaxSpeed) * autoBreakStrength;
        }
    }

    private void UpdateAxis()
    {
        verticalAxis = Input.GetAxis("Vertical");
        horizontalAxis = Input.GetAxis("Horizontal");
        handbreakAxis = Input.GetAxis("Jump");
    }

    public void Reset()
    {
        verticalAxis = 0;
        horizontalAxis = 0;
        handbreakAxis = 0;

        car.ThrottleControl = 0;
        car.SteerControl = 0;
        car.BrakeControl = 0;
    }

    public void Stop()
    {
        Reset();

        car.BrakeControl = 1;
    }
}
