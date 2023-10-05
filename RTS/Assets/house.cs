using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditorInternal.VersionControl.ListControl;
using UnityEngine.UI;

public class house : MonoBehaviour
{
    enum State { fuego, pidiendo, nada};
    [SerializeField]
    State state;
    State lastState;

    public float timer = 0;
    public float currentTime = 0;

    float waitTime = 30;
    public bool genteFuera = false;

    MaterialController player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<MaterialController>();
        state = State.nada;
        this.transform.GetChild(1).gameObject.SetActive(false);
        this.transform.GetChild(2).gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

        switch (state)
        {
            case State.fuego:
                genteFuera = true;
                if (player.GetCurrentState() == 2)
                    this.transform.GetChild(0).gameObject.SetActive(true);
                else
                    this.transform.GetChild(0).gameObject.SetActive(false);
                break; 
            case State.pidiendo:
                if (player.GetCurrentState() == 4)
                    this.transform.GetChild(0).gameObject.SetActive(true);
                else
                    this.transform.GetChild(0).gameObject.SetActive(false);

                currentTime += Time.time - timer;
                timer = Time.time;

                this.transform.GetChild(1).GetChild(0).GetComponent<Slider>().value = (float)currentTime / (float)waitTime;

                if (currentTime > waitTime && !genteFuera)
                {
                    genteFuera = true;
                    this.transform.GetChild(3).GetComponent<Citizen>().SalirAfuera();

                }

                break;
            case State.nada:
                genteFuera = false;

                break;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<MaterialController>() != null)
        {
            if ((Input.GetMouseButton(1)) && other.GetComponent<MaterialController>().GetCurrentState() == 4 && state == State.pidiendo)
            {
                other.GetComponent<MaterialController>().SetTexture(5);
                other.GetComponent<PplayerMovement>().Recoger();
                AcabarPedir();
            }
            if ((Input.GetMouseButton(1)) && other.GetComponent<MaterialController>().GetCurrentState() == 2 && state == State.fuego)
            {
                other.GetComponent<MaterialController>().SetTexture(5);
                other.GetComponent<PplayerMovement>().Recoger();
                ApagarFuego();
            }
        }
    }
    public int GetState()
    {
        return (int)state;
    }
    public void StartPedir()
    {
        this.transform.GetChild(1).gameObject.SetActive(true);
        lastState = state;
        state = State.pidiendo;
        timer = Time.time;
        currentTime = 0;
    }
    public void AcabarPedir()
    {
        this.transform.GetChild(3).GetComponent<Citizen>().VolverAdentro();
        this.transform.GetChild(1).gameObject.SetActive(false);
        this.transform.GetChild(0).gameObject.SetActive(false);

        lastState = state;
        state = State.nada;
        timer = 0;
    }

    public void ApagarFuego()
    {
        state = lastState;
        this.transform.GetChild(2).gameObject.SetActive(false);
        this.transform.GetChild(0).gameObject.SetActive(false);

        if (state == State.pidiendo)
        {
            this.transform.GetChild(1).gameObject.SetActive(true);
            timer = Time.time;

            if (timer < waitTime)
            {
                this.transform.GetChild(3).GetComponent<Citizen>().VolverAdentro();

                genteFuera = false;
            }
        }

    }
    public void PrenderFuego()
    {
        this.transform.GetChild(1).gameObject.SetActive(false);
        this.transform.GetChild(2).gameObject.SetActive(true);
        this.transform.GetChild(3).GetComponent<Citizen>().SalirAfuera();

        lastState = state;
        state = State.fuego;
    }
}
