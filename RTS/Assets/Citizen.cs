using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Citizen : MonoBehaviour
{

    public List<Transform> SpotsToGo = new List<Transform>();
    private NavMeshAgent navAgent;
    // Start is called before the first frame update
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.SetDestination(SpotsToGo[0].transform.position);
    }

    // Update is called once per frame
    void Update()
    {
    }


    public void OnConnectedToServer()
    {
        
    }
}
