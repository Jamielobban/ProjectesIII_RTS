using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comercio : MonoBehaviour
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
        if (other.GetComponent<MaterialController>() != null)
        {


            if ((Input.GetKey(KeyCode.E)) && other.GetComponent<MaterialController>().GetCurrentState() != 5)
            {
                switch(other.GetComponent<MaterialController>().GetCurrentState())
                {
                    case 0:
                        other.GetComponent<PplayerMovement>().oro += 5;

                        break;
                    case 1:

                        break;
                    case 2:

                        break;
                    case 3:

                        break;
                }
                other.GetComponent<MaterialController>().SetTexture(5);
                other.GetComponent<PplayerMovement>().Recoger();

            }
        }
    }
}
