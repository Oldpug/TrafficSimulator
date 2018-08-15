using UnityEngine;

public class Driver : MonoBehaviour
{
    [SerializeField]
    private int[] path;
    private int currentNode = 0;

    private void Start()
    {
        //here the Diriver will aquire the list of nodes from the pathfinding algorithm.
    }

    public BasicLane GetDirection()
    {
        currentNode++;
        if (currentNode >= path.Length)
        {
            return null;
        }
        else
        {
            return Map.Instance.GetNode( path[currentNode - 1] ).GetIntersectionExit( path[ currentNode ], this.transform );
        }
        
    }
}
