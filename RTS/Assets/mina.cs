using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mina : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.GetComponent<MaterialController>() != null)
        {
            if ((Input.GetKey(KeyCode.E)))
            {
                other.GetComponent<MaterialController>().SetTexture(1);
            }
        }
    }
}
