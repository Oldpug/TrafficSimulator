using System;
using UnityEngine;
using UnityEngine.UI;

public class RoadCustomizationPanel : MonoBehaviour
{
    Transform selectedObject;
    Spawner selectedSpawner;

    [SerializeField]
    InputField scaleX;

    [SerializeField]
    InputField scaleZ;

    [SerializeField]
    InputField rotationY;

    [SerializeField]
    InputField spawnRate;

    [SerializeField]
    GameObject spawnerGroup;


    public void SelectRoad(Transform obj, string tag)
    {
        selectedObject = obj;
        scaleX.text = obj.localScale.x.ToString();
        scaleZ.text = obj.localScale.z.ToString();
        rotationY.text = obj.rotation.y.ToString();
        if(tag == "spawner")
        {
            selectedSpawner = obj.GetComponentInChildren<Spawner>();
            spawnerGroup.SetActive(true);
            spawnRate.text = selectedSpawner.SpawnInterval.ToString();
        }
        else
        {
            spawnerGroup.SetActive(false);
        }
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

    public void ChangeSpawnRate(string value)
    {
        float convertedValue;
        if (Single.TryParse(value, out convertedValue))
        {
            selectedSpawner.SpawnInterval = convertedValue;
        }
    }

    public void DeleteObject()
    {
        Destroy(selectedObject.gameObject);
        gameObject.SetActive(false);
        spawnerGroup.SetActive(false);
    }
    public void HideWindow()
    {
        gameObject.SetActive(false);
        spawnerGroup.SetActive(false);
    }
}
