using UnityEngine;

[System.Serializable]
public class IntersectionExit
{
    public Transform exit;
    public BasicLane nextLane;
    public int nextIntersectionIndex;
}

public class IntersectionLane : Lane
{
    [SerializeField]
    private IntersectionExit[] intersectionExits;

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

    public IntersectionExit GetIntersectionExit(int nextIntersection)
    {
        foreach (IntersectionExit i in intersectionExits)
            if (i.nextIntersectionIndex == nextIntersection)
                return i;

        return null;
    }
}
