using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Analytics : MonoBehaviour {

    [SerializeField]
    private Text text;

    [SerializeField]
    private Text text1;

    [SerializeField]
    private Text text2;

    [SerializeField]
    private Text text3;

    [SerializeField]
    private Text text4;

    [SerializeField]
    private Text text5;

    [SerializeField]
    private Text text6;

    [SerializeField]
    private Text text7;

    [SerializeField]
    private Text text8;

    [SerializeField]
    private Text text9;

    [SerializeField]
    private Rigidbody car;
    

    float suma;
    float seconds; 
    float average;
    float still;
    float finaltimestill;
    public bool alignByGeometry;
    private Car[] cars;

    private void Start()
    {
        StartCoroutine(Watch());
        cars = FindObjectsOfType<Car>();
    }

    private IEnumerator Watch()
    {
        while (true)
        {
            foreach (Car car in cars)
            {
                suma += car.Velocity;
                yield return new WaitForSeconds(1);
            }
        }
                
    }


    void Update()
        {

        seconds += Time.deltaTime;

        //average = suma / seconds; average speed for a single car


        foreach (Car car in cars) //average speed for all the cars
        {
            suma += car.Velocity;
            if (car.Velocity == 0)
            {
                still += Time.deltaTime;
                finaltimestill += Time.deltaTime;

                text3.text = "Standing still: ";
                text8.text = "0";
                text8.text = still.ToString("0.##");
            }
            else
            {
                still = 0;
            }

        }
        
        average = suma / cars.Length;

            text.text = "Average speed: ";
            text5.text = "0";
            text5.text = average.ToString("0.##");

            text2.text = "Total time: ";
            text7.text = "0";
            text7.text = seconds.ToString("0.##");

        // finaltimestill = Mathf.Round(finaltimestill * 100f) / 100f;

        text4.text = "Total time still: ";
        text9.text = "0";
        text9.text = finaltimestill.ToString("0.##");

       }

    void OnMouseDown()
    {
        // this object was clicked - do something
        foreach (Car car in cars)
        {
            text1.text = "Speed:  ";
            text6.text = "0";
            text6.text = car.Velocity.ToString("0.##");
            Debug.Log(car.Velocity.ToString("0.##"));
        }
    }

}





