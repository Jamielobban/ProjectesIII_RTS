using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gente : MonoBehaviour
{
    public GameObject[] personas;
    public Transform[] pos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < 3 ; i++)
        {
            
            if(Vector3.Distance(personas[i].transform.position,this.transform.position) > 2)
            {

                personas[i].transform.position = Vector3.MoveTowards(personas[i].transform.position, this.transform.position, 7*Time.deltaTime);

            }
            else if (Vector3.Distance(personas[i].transform.position, pos[i].transform.position) > 0.1f)
            {
                personas[i].transform.position = Vector3.MoveTowards(personas[i].transform.position, pos[i].transform.position, 3 * Time.deltaTime);

            }
        }
    }
}
