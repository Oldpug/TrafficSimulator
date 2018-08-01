using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour
{

    [SerializeField]
    private int[] nodes;
    [SerializeField]
    private IntersectionLane[] intersections;
    private int currentNode = 0;

    private void Start()
    {
        //here the Diriver will aquire the list of nodes from the pathfinding algorithm.
    }

    public IntersectionExit GetDirection()
    {
        currentNode++;
        if (currentNode >= nodes.Length)
        {
            return null;
        }
        else
        {
            return intersections[currentNode - 1].GetIntersectionExit(nodes[currentNode]);
        }
    }
}
