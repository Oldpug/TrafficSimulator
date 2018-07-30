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
    private Rigidbody car; //luam viteza din rigibody

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

        var cars = FindObjectsOfType<Car>();
    }

    private IEnumerator Watch()
    {
        while (true)
        {
            suma += car.velocity.magnitude;
            yield return new WaitForSeconds(1);
        }
    }
    void Update()
        {

        seconds += Time.deltaTime;

        //average = suma / seconds; average speed for a single car


        foreach (Car car in cars) //average speed for all the cars
        {
          /////  suma += car.velocity.magnitude;
        }
        average = suma / cars.Length;

            text.text = "Average speed: ";
            text5.text = "0";
            text5.text = average.ToString("0.##");

            text1.text = "Speed:  ";
            text6.text = "0";
            text6.text= car.velocity.magnitude.ToString("0.##");

            text2.text = "Total time: ";
            text7.text = "0";
            text7.text = seconds.ToString("0.##");

            if (car.velocity.magnitude==0)
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
        // finaltimestill = Mathf.Round(finaltimestill * 100f) / 100f;

        text4.text = "Total time still: ";
        text9.text = "0";
        text9.text = finaltimestill.ToString("0.##");

        }

    }





