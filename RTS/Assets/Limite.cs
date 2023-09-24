using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limite : MonoBehaviour
{
    public GameObject arbol;
    public int i;
    // Start is called before the first frame update
    void Start()
    {
        for(int e = 0; e<i;e++)
        {
            GameObject a = Instantiate(arbol, this.transform);


            a.transform.position = this.transform.position + new Vector3((e%2), 0, 1.75f*e);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
