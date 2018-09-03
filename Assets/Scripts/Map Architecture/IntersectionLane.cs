using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class IntersectionExit
{
    public BasicLane[] NextLanes;

    public IntersectionLane NextIntersection;
}

public class IntersectionLane : Lane
{
    [SerializeField]
    public IntersectionExit[] IntersectionExits;

    public override Transform End
    {
        get
        {
            return null;
        }
    }

    public override Lane Next
    {
        get
        {
            return null;
        }

        set { }
    }

    public BasicLane GetIntersectionExit(IntersectionLane nextIntersection, Transform carPosition)
    {
        foreach (var i in IntersectionExits)
        {
            int randomExit = Random.Range(0, i.NextLanes.Length);
            if (i.NextIntersection == nextIntersection)
            {
                while (carPosition.forward + i.NextLanes[randomExit].End.transform.forward == Vector3.zero)
                    randomExit = Random.Range(0, i.NextLanes.Length);

                return i.NextLanes[randomExit];
            }
                
        }

        return IntersectionExits[0].NextLanes[0];
    }
}