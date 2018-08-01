using UnityEngine;

[System.Serializable]
public class IntersectionExit
{
    public BasicLane nextLane;
    public int nextIntersectionIndex;
}

public class IntersectionLane : Lane
{
    [SerializeField]
    private IntersectionExit[] intersectionExits;
    [SerializeField]
    private int IntersectionIndex;

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
    }

    public BasicLane GetIntersectionExit(int nextIntersection)
    {
        foreach (IntersectionExit i in intersectionExits)
            if (i.nextIntersectionIndex == nextIntersection)
                return i.nextLane;

        return null;
    }

    public int GetIndex()
    {
        return IntersectionIndex;
    }
}
