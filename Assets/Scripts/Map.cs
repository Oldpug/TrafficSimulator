using UnityEngine;

public class Map : MonoBehaviour
{
    private IntersectionLane[] nodes;

    private static Map instance = null;
    public static Map Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        IntersectionLane[] intersections = FindObjectsOfType<IntersectionLane>();
        nodes = new IntersectionLane[intersections.Length + 1];
        foreach(IntersectionLane intersection in intersections)
        {
            nodes[intersection.GetIndex()] = intersection;
        }
    }

    public IntersectionLane GetNode(int index)
    {
        return nodes[index];
    }
}
