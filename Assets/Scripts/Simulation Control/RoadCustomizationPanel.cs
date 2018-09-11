using System;
using UnityEngine;
using UnityEngine.UI;

public class RoadCustomizationPanel : MonoBehaviour
{
    Transform selectedObject;

    [SerializeField]
    InputField scaleX;

    [SerializeField]
    InputField scaleZ;

    [SerializeField]
    InputField rotationY;


    public void SelectRoad(Transform obj)
    {
        selectedObject = obj;
        scaleX.text = obj.localScale.x.ToString();
        scaleZ.text = obj.localScale.z.ToString();
        rotationY.text = obj.rotation.y.ToString();
    }

    public void ChangeXScale(string value)
    {
        float convertedValue;
        if(Single.TryParse(value, out convertedValue))
        {
            selectedObject.localScale = new Vector3(convertedValue, selectedObject.localScale.y, selectedObject.localScale.z);
        } 
    }

    public void ChangeYScale(string value)
    {
        float convertedValue;
        if (Single.TryParse(value, out convertedValue))
        {
            selectedObject.localScale = new Vector3(selectedObject.localScale.x, selectedObject.localScale.y, convertedValue);
        }
    }

    public void ChangeRotation(string value)
    {
        float convertedValue;
        if (Single.TryParse(value, out convertedValue))
        {
            selectedObject.rotation = Quaternion.Euler(new Vector3(0, convertedValue, 0));
        }
    }

    public void DeleteObject()
    {
        Destroy(selectedObject.gameObject);
        gameObject.SetActive(false);
    }
    public void HideWindow()
    {
        gameObject.SetActive(false);
    }
}
