using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class IntersectionExit
{
    [SerializeField]
    public BasicLane[] Lanes;
}

public class IntersectionLane : Lane
{
    [SerializeField]
    public IntersectionExit[] Exits;

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

    public BasicLane GetRandomExit(Transform carPosition)
    {
        var exitIdx = Random.Range(0, Exits.Length);
        var exit = Exits[exitIdx].Lanes;

        while (carPosition.forward + exit[0].End.transform.forward == Vector3.zero)
        {
            exitIdx = Random.Range(0, Exits.Length);
            exit = Exits[exitIdx].Lanes;
        }

        var lane = Random.Range(0, exit.Length);
        return exit[lane];
    }
}