using UnityEngine;

public class InterfaceManager : MonoBehaviour
{
    public bool buildMode;

    [SerializeField]
    CameraControler cameraControl;

    [SerializeField]
    GameObject buildPanel;
    [SerializeField]
    GameObject analyticsPanel;
    Linker[] linkers;

    private void Start()
    {
        cameraControl = FindObjectOfType<CameraControler>();
        linkers = FindObjectsOfType<Linker>();
        foreach (Linker linker in linkers)
        {
            linker.gameObject.SetActive(false);
        }
    }

    public void SpawnRoad(RoadStructure road)
    {
        Instantiate(road);
        road.OnMouseDown();
        road.interfaceManager = this;
        road.transform.parent = null;
    }

    public void EnableRoadPanel(Transform road)
    {

    }

    public void ToggleBuildMode()
    {
        cameraControl.SwitchMode();
        if(buildMode)
        {
            buildPanel.SetActive(false);
        }
        else
        {
            buildPanel.SetActive(true);
            analyticsPanel.SetActive(false);
        }
        
        buildMode = !buildMode;
        ToggleLinkingInterface(buildMode);

    }
    
    public void ToggleLinkingInterface(bool value)
    {
        if(!value)
        {
            linkers = FindObjectsOfType<Linker>();
        }
        foreach(Linker linker in linkers)
        {
            linker.gameObject.SetActive(value);
        }
    }
}
