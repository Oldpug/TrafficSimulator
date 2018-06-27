using UnityEngine;

public class StopCar : MonoBehaviour
{
    private Rigidbody body;
    private Car car;
    public bool IsStopped = false;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
        car = GetComponent<Car>();
    }

    public void Button_Click()
    {
        if (IsStopped == true)
        {
            car.enabled = true;
            IsStopped = false;
        }
        else
        {
            body.velocity = Vector3.zero;
            IsStopped = true;
            car.enabled = false;
        }
    }
}


