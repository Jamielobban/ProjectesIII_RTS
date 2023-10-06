using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class molino : MonoBehaviour
{

    bool recoger = false;
    bool control = false;
    public bool creciendo = false;
    float time;
    public float espera;
    MaterialController player;
    public int type;
    int recursos = 0;
    // Start is called before the first frame update
    void Start()
    {
        recoger = false;
        this.transform.GetChild(1).gameObject.SetActive(false);
        player = FindObjectOfType<MaterialController>();
        this.transform.GetChild(3).gameObject.SetActive(false);
        this.transform.GetChild(4).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetCurrentState() == type && !recoger && !creciendo)
            this.transform.GetChild(2).GetChild(0).gameObject.SetActive(true);
        else
            this.transform.GetChild(2).GetChild(0).gameObject.SetActive(false);


        if ((Input.GetMouseButtonUp(1)) && control)
        {
            control = false;
        }
        if ((Input.GetMouseButtonUp(0)) && control)
        {
            control = false;
        }
        if (creciendo)
        {
            float value = (float)(Time.time - time) / espera;
            this.transform.GetChild(1).GetChild(0).GetComponent<Slider>().value = value;
        }
        if (creciendo && (Time.time - time) > espera)
        {
            creciendo = false;
            recoger = true;
            this.transform.GetChild(1).gameObject.SetActive(false);
            this.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Start");
            recursos = 0;


        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<MaterialController>() != null)
        {

            if ((Input.GetMouseButton(0)) && !recoger && !control && other.GetComponent<MaterialController>().GetCurrentState() == 3 && !creciendo && other.GetComponent<PplayerMovement>().canMove)
            {
                control = true;
                other.GetComponent<MaterialController>().SetTexture(5);
                other.GetComponent<PplayerMovement>().Recoger();
                this.transform.GetChild(1).gameObject.SetActive(true);
                time = Time.time;
                creciendo = true;
                //this.transform.GetChild(4).gameObject.SetActive(true);

            }
            else if ((Input.GetMouseButton(1)) && recoger && !control && other.GetComponent<MaterialController>().GetCurrentState() == 5 && other.GetComponent<PplayerMovement>().canMove)
            {
                control = true;
                other.GetComponent<MaterialController>().SetTexture(4);
                other.GetComponent<PplayerMovement>().Recoger();
                this.transform.GetChild(0).GetComponent<Animator>().SetTrigger("End");
                this.transform.GetChild(3).gameObject.SetActive(false);
                this.transform.GetChild(4).gameObject.SetActive(false);
                recoger = false;
            }
            //else if(recursos == 0 && (Input.GetMouseButton(1)) && other.GetComponent<MaterialController>().GetCurrentState() == 3 && !creciendo && other.GetComponent<PplayerMovement>().canMove && !recoger)
            //{
            //    this.transform.GetChild(3).gameObject.SetActive(true);
            //    control = true;
            //    other.GetComponent<MaterialController>().SetTexture(5);
            //    other.GetComponent<PplayerMovement>().Recoger();
            //    recursos = 1;
            //}

        }
    }
}
