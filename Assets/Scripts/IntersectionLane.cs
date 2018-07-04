using System.Collections.Generic;
using UnityEngine;

public class IntersectionLane : Lane
{

    [SerializeField]
    private List<Lane> possibleNextLanes = new List<Lane>();
    private int nextLaneIndex;

    private void Awake()
    {
        nextLaneIndex = Random.Range(0, possibleNextLanes.Count);
    }

    public override Transform End
    {
        get
        {
            return possibleNextLanes[nextLaneIndex].transform;
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
