using System.Collections;
using System.Collections.Generic;
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
    private Rigidbody Car; //luam viteza din rigibody

    float suma;
    float seconds; 
    float average;
    float still;
        void Update()
        {
        suma += Car.velocity.magnitude; //velocity e vector 3 nu returneaza int sau float, that's why we use magnitude 
        seconds += Time.deltaTime; 
        average = suma / seconds;
        text.text = "Av speed: " + average.ToString();
        text1.text = "Speed:  " + Car.velocity.magnitude.ToString();
        text2.text = "Total time: " + seconds.ToString();
        if (Car.velocity.magnitude==0)
        {
            still += Time.deltaTime;
            text3.text = "Standing still: " + still.ToString();
        }
        else
        {
            still = 0;
        }
        }

    }





