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
    private Text text4;

    [SerializeField] 
    private Rigidbody Car; //luam viteza din rigibody

    float suma;
    float seconds; 
    float average;
    float still;
    float finaltimestill;
    private void Start()
    {
        StartCoroutine(Watch());
    }

    private IEnumerator Watch()
    {
        while (true)
        {
            suma += Car.velocity.magnitude;
            yield return new WaitForSeconds(1);
        }
    }
    void Update()
        {
              
            seconds += Time.deltaTime; 
            average = suma / seconds;
            text.text = "Av speed: " + average.ToString("0.##");
            text1.text = "Speed:  " + Car.velocity.magnitude.ToString("0.##");
            text2.text = "Total time: " + seconds.ToString("0.##");

            if (Car.velocity.magnitude==0)
            {
                still += Time.deltaTime;
                finaltimestill += Time.deltaTime;
                text3.text = "Standing still: " + still.ToString("0.##");
            }
            else
            {
                still = 0;
            }
            // finaltimestill = Mathf.Round(finaltimestill * 100f) / 100f;
            text4.text = "Total time still: " + finaltimestill.ToString("0.##");
        }

    }





