using UnityEngine;

public class CarInputControl : MonoBehaviour
{
    [SerializeField] private Car car;

    private void Update()
    {
        car.throttleControl = Input.GetAxis("Vertical");
        car.steerControl = Input.GetAxis("Horizontal");
        car.brakeControl = Input.GetAxis("Jump");
    }
}
