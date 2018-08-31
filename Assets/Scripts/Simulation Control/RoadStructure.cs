using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadStructure : MonoBehaviour {

    [SerializeField]
    public Transform linkingInterface;

    public InterfaceManager interfaceManager;

    private Vector3 draggingOffset;
    private Vector3 screenPoint;

    public void OnMouseDown()
    {
        interfaceManager.EnableRoadPanel(transform);
        if(interfaceManager.buildMode)
        {
            screenPoint = Camera.main.WorldToScreenPoint(transform.position);
            draggingOffset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        }
    }

    public void OnMouseDrag()
    {
        if (interfaceManager.buildMode)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + draggingOffset;
            transform.position = curPosition;
        }
    }
}
