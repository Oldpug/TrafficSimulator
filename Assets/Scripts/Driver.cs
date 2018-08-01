using UnityEngine;

public class Driver : MonoBehaviour
{
    [SerializeField]
    private int[] nodes;
    private int currentNode = 0;

    private void Start()
    {
        //here the Diriver will aquire the list of nodes from the pathfinding algorithm.
    }

    public BasicLane GetDirection()
    {
        currentNode++;
        if (currentNode >= nodes.Length)
        {
            return null;
        }
        else
        {
            return Map.Instance.intersections[ nodes[currentNode - 1] ].GetIntersectionExit( nodes[ currentNode ] );
        }
    }
}
