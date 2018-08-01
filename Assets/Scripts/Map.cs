using UnityEngine;

public class Map : MonoBehaviour
{
    public IntersectionLane[] intersections;

    private static Map instance;
    public static Map Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Map();
            }
            return instance;
        }
    }

    private Map()
    {
        IntersectionLane[] nodes = FindObjectsOfType<IntersectionLane>();
        intersections = new IntersectionLane[nodes.Length + 1];

        foreach (IntersectionLane node in nodes)
        {
            intersections[node.GetIndex()] = node;
        }
    }
}
