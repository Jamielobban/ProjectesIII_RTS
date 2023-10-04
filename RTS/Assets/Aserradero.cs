using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Aserradero : MonoBehaviour
{
    public GameObject canvas;
    public bool recoger = false;
    public bool farmeando = false;
    public bool control = false;
    public GameObject madera;
    float position = 0;
    float objectPosition = 0;

    bool bloquear = false;
    public float value = 0;
    public float maxValue = 10000;

    // Start is called before the first frame update
    void Start()
    {
        madera.SetActive(false);
        canvas.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetMouseButtonUp(1)) && control)
        {
            control = false;
        }
        if (Input.GetMouseButtonDown(0) && farmeando && !bloquear)
        {
            bloquear = true;
            position = Input.mousePosition.x;
        }

        if (Input.GetMouseButtonUp(0)&&bloquear)
        {
            bloquear = false;
        }

        if (Input.GetMouseButton(0)&&farmeando)
        {
            objectPosition += (Input.mousePosition.x - position)/10;
            if(objectPosition < -45)
                objectPosition = -45;
            else if(objectPosition > 45)
                objectPosition = 45;

            madera.transform.GetChild(0).transform.localPosition = new Vector3(objectPosition, 41, 0);
            value += Mathf.Abs(Input.mousePosition.x - position);

            float slider = (float)value / (float)maxValue;
            canvas.transform.GetChild(0).GetComponent<Slider>().value = slider;

            if (value > maxValue)
            {
                value = 0;
                farmeando = false;
                recoger = true;
                Salir();
                GameObject.FindObjectOfType<PplayerMovement>().Salir();
                canvas.transform.GetChild(1).gameObject.SetActive(true);
                madera.transform.GetChild(0).transform.localPosition = new Vector3(0, 41, 0);
                canvas.transform.GetChild(0).GetComponent<Slider>().value = 0;

            }

            position = Input.mousePosition.x;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<MaterialController>() != null)
        {
            canvas.SetActive(true);


            if ((Input.GetMouseButton(1)) && !recoger && !farmeando && !control)
            {
                control = true;
                farmeando = true;
                other.GetComponent<PplayerMovement>().Farmear();

                madera.SetActive(true);

            }
            else if ((Input.GetMouseButton(1)) && farmeando && !control)
            {
                farmeando = false;
                control = true;

                Salir();
                other.GetComponent<PplayerMovement>().Salir();
            }

            if ((Input.GetMouseButton(1)) && other.GetComponent<MaterialController>().GetCurrentState() == 5 && recoger && other.GetComponent<PplayerMovement>().canMove)
            {
                control = true;
                canvas.transform.GetChild(1).gameObject.SetActive(false);

                other.GetComponent<MaterialController>().SetTexture(0);
                other.GetComponent<PplayerMovement>().Recoger();
                recoger = false;


            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<MaterialController>() != null)
        {
            canvas.SetActive(false);

        }
    }
    void Salir()
    {
        madera.SetActive(false);

    }
}
