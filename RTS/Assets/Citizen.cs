using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Citizen : MonoBehaviour
{

    public List<Transform> SpotsToGo = new List<Transform>();
    private NavMeshAgent navAgent;
    public int currentPoint;
    // Start is called before the first frame update
    void Start()
    {
        currentPoint = Random.RandomRange(0, 3);
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.SetDestination(SpotsToGo[currentPoint].transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(this.transform.position, SpotsToGo[currentPoint].transform.position)< 5)
        {
            int a = currentPoint;
            currentPoint = Random.RandomRange(0, 3);

            while(a != currentPoint)
            {
                currentPoint = Random.RandomRange(0, 3);

            }
            navAgent.SetDestination(SpotsToGo[currentPoint].transform.position);

        }

    }


    public void OnConnectedToServer()
    {
        
    }
}
