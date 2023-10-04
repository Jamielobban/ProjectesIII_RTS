using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pozo : MonoBehaviour
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
            if ((Input.GetMouseButton(1)) && other.GetComponent<MaterialController>().GetCurrentState() == 5 && other.GetComponent<PplayerMovement>().canMove)
            {
                other.GetComponent<MaterialController>().SetTexture(2);
                other.GetComponent<PplayerMovement>().Recoger();
            }

        }
    }
}
