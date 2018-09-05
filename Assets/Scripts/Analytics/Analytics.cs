using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Analytics : MonoBehaviour
{
    [SerializeField]
    private Text averageSpeed;

    [SerializeField]
    private Text TimeLeft;

    [SerializeField]
    private Text carsStandingStill;

    [SerializeField]
    private Text totalTimeStill;

    [SerializeField]
    private Text SimulationTime;

    [SerializeField]
    private Text CO2Text;

    float totalSum;

    float seconds;
    float hour;
    float minutes;
    float average;
    float totaltimestill;
    private CarAnalytics[] cars;
    float CarbonEmissions;

   /* public InputField HourText;
    public InputField MinutesText;
    public InputField SecondsText;*/

    private void Start()
    {
        cars = FindObjectsOfType<CarAnalytics>();
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
            float suma = 0;
            float CO2Sum = 0;
            int stoppedCars = 0;
            foreach (CarAnalytics car in cars) //average speed for all the cars
            {
                CO2Sum += car.CO2;
                suma += car.Velocity;
                if (car.Velocity == 0)
                {
                    stoppedCars++;
                    totaltimestill++;                  
                }

            }

            CarbonEmissions = CO2Sum / seconds;
            average = totalSum / seconds;

            CO2Text.text = CarbonEmissions.ToString("0.##");
            carsStandingStill.text = stoppedCars.ToString(); 
            averageSpeed.text = average.ToString("0.##");
            totalTimeStill.text = totaltimestill.ToString("0.##");
        }
    }

    private void Update()
    {
        TimeLeft.text = seconds.ToString("0.##");
        if (Time.timeScale > 0)
        {
            seconds += Time.deltaTime;
        }

    }

}
