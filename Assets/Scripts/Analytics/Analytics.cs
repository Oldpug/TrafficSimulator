using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Analytics : MonoBehaviour
{
    [SerializeField]
    private Text averageSpeed;

    [SerializeField]
    private Text carsStandingStill;

    [SerializeField]
    private Text totalTimeStill;

    [SerializeField]
    private Text TotalTime;

    [SerializeField]
    private Text CO2Text;

    float timpDeLaInceputulLumii;
    float sumaDeLaInceputulLumii;
    float hour;
    float minutes;
    float average;
    float totaltimestill;
    private List<CarAnalytics> cars;
    float CarbonEmissions;

   /* public InputField HourText;
    public InputField MinutesText;
    public InputField SecondsText;*/

    private void Start()
    {
        cars = FindObjectsOfType<CarAnalytics>().ToList();
        StartCoroutine(Watch());
        /*HourText.text = hour.ToString();
        MinutesText.text = minutes.ToString();
        SecondsText.text = seconds.ToString();*/
    }

    private IEnumerator Watch()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            float sumaDinAcestFrame = 0;
            float CO2Sum = 0;
            int stoppedCars = 0;
            foreach (CarAnalytics car in cars) //average speed for all the cars
            {
                CO2Sum += car.CO2;
                sumaDinAcestFrame += car.Velocity*10;
                Debug.Log(car.Velocity);
                if (car.Velocity == 0)
                {
                    stoppedCars++;
                    totaltimestill++;                  
                }

            }

            CarbonEmissions = CO2Sum / timpDeLaInceputulLumii;

            sumaDeLaInceputulLumii += sumaDinAcestFrame;
            average = sumaDeLaInceputulLumii / timpDeLaInceputulLumii;

            CO2Text.text = CarbonEmissions.ToString("0.##");
            carsStandingStill.text = stoppedCars.ToString(); 
            averageSpeed.text = average.ToString("0.##");
            totalTimeStill.text = totaltimestill.ToString("0.##");


}
    }


    public void AddCar(Car c) { cars.Add(c.GetComponent<CarAnalytics>()); }
    public void RemoveCar(Car c) { cars.Remove(c.GetComponent<CarAnalytics>()); }

    private void Update()
    {
        TotalTime.text = timpDeLaInceputulLumii.ToString("0.##");
        if (Time.timeScale > 0)
        {
            timpDeLaInceputulLumii += Time.deltaTime;
        }

    }

}
