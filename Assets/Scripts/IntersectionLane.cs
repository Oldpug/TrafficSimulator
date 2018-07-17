using System.Collections.Generic;
using UnityEngine;

public class IntersectionLane : Lane
{

    [SerializeField]
    private List<Lane> possibleNextLanes = new List<Lane>();
    [SerializeField]
    private List<GameObject> possibleEndLanes = new List<GameObject>();
    private int nextLaneIndex;
    private int nextEndIndex;

    private void Awake()
    {
        nextLaneIndex = Random.Range(0, possibleNextLanes.Count);
        nextEndIndex = Random.Range(0, possibleEndLanes.Count);
    }

    public override Transform End
    {
        get
        {
            return possibleEndLanes[nextEndIndex].transform;
        }
    }

    public override Lane Next
    {
        get
        {
            
            return possibleNextLanes[nextLaneIndex];
        }
    }
}
