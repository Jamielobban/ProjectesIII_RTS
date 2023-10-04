using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Cultivos : MonoBehaviour
{
    bool recoger;
    public bool control = false;
    public bool creciendo = false;
    float time;
    public float espera;
    MaterialController player;
    public int type;
    // Start is called before the first frame update
    void Start()
    {
        recoger = false;
        this.transform.GetChild(0).gameObject.SetActive(false);
        this.transform.GetChild(1).gameObject.SetActive(false);
        player = FindObjectOfType<MaterialController>();

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
        if(creciendo)
        {
            float value = (float)(Time.time - time) / espera;
            this.transform.GetChild(1).GetChild(0).GetComponent<Slider>().value = value;
        }
        if(creciendo&&(Time.time-time)> espera)
        {
            creciendo = false;
            recoger = true;
            this.transform.GetChild(0).gameObject.SetActive(true);
            this.transform.GetChild(1).gameObject.SetActive(false);

        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<MaterialController>() != null)
        {
            if ((Input.GetMouseButton(1)) && !recoger && !control && other.GetComponent<MaterialController>().GetCurrentState() == 2 && !creciendo)
            {
                control = true;
                other.GetComponent<MaterialController>().SetTexture(5);
                other.GetComponent<PplayerMovement>().Recoger();
                creciendo = true;
                time = Time.time;
                this.transform.GetChild(1).gameObject.SetActive(true);
            }
            else if ((Input.GetMouseButton(1)) && recoger && !control && other.GetComponent<MaterialController>().GetCurrentState() == 5 && other.GetComponent<PplayerMovement>().canMove)
            {
                control = true;
                other.GetComponent<MaterialController>().SetTexture(3);
                other.GetComponent<PplayerMovement>().Recoger();
                this.transform.GetChild(0).gameObject.SetActive(false);
                recoger = false;
            }
        }
    }
}
