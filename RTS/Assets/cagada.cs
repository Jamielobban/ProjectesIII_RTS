using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cagada : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < this.transform.childCount;i++)
        {
            this.transform.GetChild(i).transform.position += new Vector3(0,0,-0.02f*(int)(i/37));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
