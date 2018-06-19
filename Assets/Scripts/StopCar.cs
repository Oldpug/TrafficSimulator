using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StopCar : MonoBehaviour
{
    private Rigidbody body;
    private Car car;
    public bool theCarIsStopped = false;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
        car = GetComponent<Car>();
    }
    public void Button_Click()
    {
        if (theCarIsStopped==true)
        {
            car.enabled = true;
            theCarIsStopped = false;
        }
        else
        {
            body.velocity = Vector3.zero;
            theCarIsStopped = true;
            car.enabled = false;
        }
       
        
    }

}


