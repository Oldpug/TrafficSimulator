using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{

    private Transform carriedObject;
    private Linker linkingObject;
    private bool carrying;
    private bool linking;

	void Start ()
    {

	}
	
	void Update ()
    {
        if(Input.GetMouseButtonDown(1) && !carrying && !linking)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if(hit.transform.gameObject.tag == "ROAD")
                {
                    carriedObject = hit.transform;
                    carrying = true;
                }
                if(hit.transform.gameObject.tag == "LINKER")
                {
                    linkingObject = hit.transform.GetComponent<Linker>();
                }
            }
        }
        if (Input.GetMouseButtonDown(0) && carrying)
        {
            carrying = false;
        }
        if (Input.GetMouseButtonUp(0) && linking)
        {
            linking = false;

        }
	}

    public void PickUp(Transform obj)
    {
        carriedObject = obj;
        carrying = true;
    }
}
