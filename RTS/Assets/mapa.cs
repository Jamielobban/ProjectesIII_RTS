using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapa : MonoBehaviour
{
    public GameObject[] piezas;
    // Start is called before the first frame update
    void Start()
    {


        for(int i = 0; i < 28; i++)
        {
            int startPos = (i % 2) == 0 ? 0 : 1;
            for (int e = 0; e < 37; e++)
            {
                float[] pos = new float[6];
                GameObject a = Instantiate(piezas[0],this.transform);


                a.transform.position = this.transform.position + new Vector3((startPos+(e*2)), 0, i*1.75f);

                







            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
