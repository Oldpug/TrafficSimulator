using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntersectionCustomizationPanel : RoadCustomizationPanel
{
    TrafficLights selectedIntersection;

    public void SelectIntersection(TrafficLights intersection)
    {
        selectedIntersection = intersection;
    }
	
}
