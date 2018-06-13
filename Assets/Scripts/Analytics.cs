using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptCami : MonoBehaviour {

    [SerializeField] 
    private Rigidbody Car; //luam viteza din rigibody
    float suma; //suma tuturor vitezelor
    float seconds;  //secundele
    float average;  //media suma/timp
        void Update()
        {
        suma += Car.velocity.magnitude; //car e vector 3 nu returneaza int sau float, deci folosim magnitude 
        seconds += Time.deltaTime; //este o functie time care ia secunde
        average = suma / seconds;
        Debug.Log(average); //afisam in consola sa vedem daca merge
        }
    }





