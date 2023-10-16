using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Citizen : MonoBehaviour
{

    public List<Transform> SpotsToGo = new List<Transform>();
    public Transform house;
    private NavMeshAgent navAgent;
    public int currentPoint;
    public bool volver = false;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.GetChild(1).position = house.position;
        if (SpotsToGo.Count != 0)
        {
            currentPoint = Random.RandomRange(0, SpotsToGo.Count);

        }
        navAgent = this.transform.GetChild(1).GetComponent<NavMeshAgent>();
        VolverAdentro();

    }

    // Update is called once per frame
    void Update()
    {
        if (volver)
        {
            if (Vector3.Distance(this.transform.GetChild(1).position, house.position) < 0.5f)
            {
                this.transform.GetChild(1).gameObject.SetActive(false);

            }
        }
        else
        {
            if(SpotsToGo.Count != 0)
            {

                if (Vector3.Distance(this.transform.GetChild(1).position, SpotsToGo[currentPoint].transform.position) < 0.5f)
                {
                    //Debug.Log("sgfsdg");

                    int a = currentPoint;
                    currentPoint = Random.RandomRange(0, SpotsToGo.Count);

                    while (a == currentPoint)
                    {
                        currentPoint = Random.RandomRange(0, SpotsToGo.Count);

                    }
                    navAgent.SetDestination(SpotsToGo[currentPoint].transform.position);

                }
            }

        }
    }

    public void SalirAfuera()
    {
        this.transform.GetChild(1).gameObject.SetActive(true);

        navAgent.SetDestination(SpotsToGo[currentPoint].transform.position);

        volver = false;
    }
    public void VolverAdentro()
    {
        volver = true;
        navAgent.SetDestination(house.position);
    }

    public void OnConnectedToServer()
    {
        
    }
}
