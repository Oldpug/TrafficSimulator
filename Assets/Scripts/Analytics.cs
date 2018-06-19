using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Analytics : MonoBehaviour {

    [SerializeField]
    private Text text;

    [SerializeField] 
    private Rigidbody Car; //luam viteza din rigibody
    float suma;
    float seconds; 
    float average;  
        void Update()
        {
        suma += Car.velocity.magnitude; //velocity e vector 3 nu returneaza int sau float, that's why we use magnitude 
        seconds += Time.deltaTime; 
        average = suma / seconds;
        text.text = average.ToString();
        }

    }





