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
    private Rigidbody Car; //luam viteza din rigibody

    float suma;
    float seconds; 
    float average;
    float still;
    float finaltimestill;
    public bool alignByGeometry;

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
            text.text = "Average speed: ";
            text5.text = "0";
            text5.text = average.ToString("0.##");
            text1.text = "Speed:  ";
            text6.text = "0";
            text6.text= Car.velocity.magnitude.ToString("0.##");
            text2.text = "Total time: ";
            text7.text = "0";
            text7.text = seconds.ToString("0.##");

            if (Car.velocity.magnitude==0)
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





