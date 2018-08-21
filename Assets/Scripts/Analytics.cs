using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Analytics : MonoBehaviour
{
    [SerializeField]
    private Text averageSpeed;

    [SerializeField]
    private Text currentSpeed;

    [SerializeField]
    private Text totalTime;

    [SerializeField]
    private Text carsStandingStill;

    [SerializeField]
    private Text totalTimeStill;

    float totalSum;
    float seconds = 0;
    float averageSpeedAtThisMoment;
    float averageSinceStart;
    float totaltimestill;
    private Car[] cars;

    private void Start()
    {
        cars = FindObjectsOfType<Car>();
        StartCoroutine(Watch());
        seconds = 0;
    }

    private IEnumerator Watch()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            float suma = 0;
            int stoppedCars = 0;
            foreach (Car car in cars) //average speed for all the cars
            {                
                suma += car.Velocity;
                if (car.Velocity == 0)
                {
                    stoppedCars++;
                    totaltimestill++;                  
                }

            }
            

            averageSpeedAtThisMoment = suma / cars.Length;
            totalSum += averageSpeedAtThisMoment;
            averageSinceStart = totalSum / seconds;

            carsStandingStill.text = stoppedCars.ToString(); 
            averageSpeed.text = averageSinceStart.ToString("0.##");
            currentSpeed.text = averageSpeedAtThisMoment.ToString("0.##");
            totalTimeStill.text = totaltimestill.ToString("0.##");
        }
    }

    private void Update()
    {
        totalTime.text = seconds.ToString("0.##");
        if (Time.timeScale > 0)
        {
            seconds += Time.deltaTime;
        }
    }

    void OnMouseDown()
    {
        // this object was clicked - do something pls
        foreach (Car car in cars)
        {
            Debug.Log(car.Velocity.ToString("0.##"));

            currentSpeed.text = car.Velocity.ToString("0.##");
        }
    }

}





