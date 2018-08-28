using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class IntersectionExit
{
    public BasicLane[] nextLane;

    public int nextIntersectionIndex;
}

public class IntersectionLane : Lane
{
    [SerializeField]
    private readonly IntersectionExit[] intersectionExits;

    [SerializeField]
    private readonly int IntersectionIndex;

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

    public BasicLane GetIntersectionExit(int nextIntersection, Transform carPosition)
    {
        foreach (var i in intersectionExits)
            if (i.nextIntersectionIndex == nextIntersection && carPosition.forward + i.nextLane[Random.Range(0, i.nextLane.Length)].End.transform.forward != Vector3.zero)
                return i.nextLane[Random.Range(0, i.nextLane.Length)];

        return intersectionExits[0].nextLane[0];
    }

    public int GetIndex()
    {
        return IntersectionIndex;
    }
}