using UnityEngine;
using UnityEngine.UI;

public class StopCar : MonoBehaviour
{
    [SerializeField]
    private Text buttontext;

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
            buttontext.text = "Stop the car";
            car.enabled = true;
            IsStopped = false;
            
        }
        else
        {
            buttontext.text = "Start the car";
            body.velocity = Vector3.zero;
            IsStopped = true;
            car.enabled = false;
        }
    }
}


