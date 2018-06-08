using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarNavmash : MonoBehaviour
{   
    [SerializeField]
    private Transform destination;
    private NavMeshAgent agent;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = destination.position;
    }
}
