using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mina : MonoBehaviour
{
    public GameObject canvas;
    bool recoger = false;
    public bool farmeando = false;
    public GameObject boton;
    public bool control = false;
    public bool bloquear = false;
    public int impactos = 0;
    public int impactoRomper = 4;

    // Start is called before the first frame update
    void Start()
    {
        boton.SetActive(false);
        canvas.SetActive(false);
        canvas.transform.GetChild(1).gameObject.SetActive(false);
        canvas.transform.GetChild(0).GetComponent<Slider>().value = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetMouseButtonUp(1)) && control)
        {
            control = false;
        }
        if ((Input.GetMouseButtonUp(0)) && control)
        {
            control = false;
        }
    }
    
    private void OnTriggerStay(Collider other)
    {
        if(other.GetComponent<MaterialController>() != null)
        {
            canvas.SetActive(true);


            if ((Input.GetMouseButton(0)) && !recoger && !farmeando&& !control)
            {
                control = true;
                farmeando = true;
                other.GetComponent<PplayerMovement>().Farmear();

                StartCoroutine(picar());
            }
            else if ((Input.GetMouseButton(1)) && farmeando && !control)
            {
                farmeando = false;
                bloquear = true;
                control = true;

                Salir();
                other.GetComponent<PplayerMovement>().Salir();
            }

            if ((Input.GetMouseButton(1)) && other.GetComponent<MaterialController>().GetCurrentState() == 5 && recoger && other.GetComponent<PplayerMovement>().canMove)
            {
                control = true;
                canvas.transform.GetChild(1).gameObject.SetActive(false);

                other.GetComponent<MaterialController>().SetTexture(1);
                other.GetComponent<PplayerMovement>().Recoger();
                recoger = false;
                this.transform.GetChild(0).GetChild(0).GetChild(3).gameObject.SetActive(false);

            }
        }
    }
    private IEnumerator picar()
    {
        boton.SetActive(true);
        boton.transform.position = new Vector3(Random.RandomRange(Screen.width / 8, Screen.width - (Screen.width / 8)), Random.RandomRange(Screen.height / 8, Screen.height - (Screen.height / 8)), 0);

        yield return new WaitForSeconds(1f);
        if (farmeando)
        {
            if (!bloquear)
            {
                StartCoroutine(picar());
            }
            else
            {
                bloquear = false;

            }
        }
        else
        {
            bloquear = false;

        }

    }

    void Salir()
    {
        boton.SetActive(false);

    }
    public void farmear()
    {
        impactos++;
        canvas.transform.GetChild(0).GetComponent<Slider>().value = (float)impactos / (float)impactoRomper;

        bloquear = true;
        if(impactos < impactoRomper)
            StartCoroutine(picar());
        else
        {


            impactos = 0;
            canvas.transform.GetChild(0).GetComponent<Slider>().value = (float)impactos / (float)impactoRomper;
            canvas.transform.GetChild(1).gameObject.SetActive(true);

            farmeando = false;
            recoger = true;
            Salir();
            FindAnyObjectByType<PplayerMovement>().Salir();
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<MaterialController>() != null)
        {
            canvas.SetActive(false);

        }
    }
}
