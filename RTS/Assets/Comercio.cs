using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comercio : MonoBehaviour
{

    MaterialController player;
    public int type;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<MaterialController>();
        this.transform.parent.GetChild(1).gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetCurrentState() != type)
            this.transform.parent.GetChild(2).GetChild(0).gameObject.SetActive(true);
        else
            this.transform.parent.GetChild(2).GetChild(0).gameObject.SetActive(false);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<MaterialController>() != null)
        {
            this.transform.parent.GetChild(1).gameObject.SetActive(true);


            if ((Input.GetMouseButton(1)) && other.GetComponent<MaterialController>().GetCurrentState() != 5 && other.GetComponent<PplayerMovement>().canMove)
            {
                switch(other.GetComponent<MaterialController>().GetCurrentState())
                {
                    case 0:
                        other.GetComponent<PplayerMovement>().oro += 2;

                        break;
                    case 1:
                        other.GetComponent<PplayerMovement>().oro += 2;

                        break;
                    case 2:
                        other.GetComponent<PplayerMovement>().oro += 1;

                        break;
                    case 3:
                        other.GetComponent<PplayerMovement>().oro += 4;

                        break;
                    case 4:
                        other.GetComponent<PplayerMovement>().oro += 10;

                        break;
                }
                other.GetComponent<MaterialController>().SetTexture(5);
                other.GetComponent<PplayerMovement>().Recoger();

            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<MaterialController>() != null)
        {
            this.transform.parent.GetChild(1).gameObject.SetActive(false);

        }
    }
}
