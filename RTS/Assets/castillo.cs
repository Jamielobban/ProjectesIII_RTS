using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class castillo : MonoBehaviour
{
    [SerializeField]
    public Slider oro;
    public Slider tiempo;

    public float rellenarBarra = 1;
    public float velocidadVaciar = 0.01f;
    float timer = 0;
    public float oroTotal = 0;
    public float oroMax = 100;
    public int sceneNumber;

    public PplayerMovement player;
    bool acabado;
    public GameObject menu;
    public GameObject menu1;
    public GameObject menu2;

    public TMP_Text tiempoTotal;
    public TMP_Text tiempoActual;
    public TMP_Text tiempoActualMenu;

    public TMP_Text oroTotalUI;
    public TMP_Text oroActual;
    public TMP_Text oroActualMenu;
    public TMP_Text oroMaxMenu;
    int tiempoInt;
    float tiempoStart;

    public int tiempoTotalInt;

    // Start is called before the first frame update
    void Start()
    {
        tiempoStart = Time.time;
        acabado = false;
        menu.SetActive(false);
        menu1.SetActive(false);
        menu2.SetActive(false);

    }

    public void recoger()
    {
        oroTotal += 5;
        if(oroTotal >= oroMax)
        {
            oroTotal = oroMax;
            acabado = true;
            player.Acabar();
        }
        oro.value = (float)((float)oroTotal / (float)oroMax);



    }
    // Update is called once per frame
    void Update()
    {
        if(tiempoInt != tiempoTotalInt && !acabado)
        tiempoInt = (int)(Time.time - tiempoStart);

        tiempoTotal.text = tiempoTotalInt.ToString();
        tiempoActual.text = tiempoInt.ToString();
        tiempoActualMenu.text = tiempoInt.ToString();
       
        oroTotalUI.text = oroMax.ToString();
        oroActual.text = oroTotal.ToString();
        oroActualMenu.text = oroTotal.ToString();
        oroMaxMenu.text = oroMax.ToString();

        if(!acabado)
        {
            rellenarBarra -= velocidadVaciar * Time.deltaTime;
            tiempo.value = rellenarBarra;

            if (tiempoTotalInt <= tiempoInt)
            {
                acabado = true;
                player.Acabar();

            }

        }
        else
        {
            if(player.HaLlegado())
            {
                if (oroTotal == oroMax)
                {
                    menu.SetActive(true);
                    menu1.SetActive(true);

                }
                else if (oroTotal < oroMax)
                {
                    menu.SetActive(true);
                    menu2.SetActive(true);

                }

                Invoke("cambiar", 3);
            }

        }




    }
    void cambiar()
    {
        if (oroTotal == oroMax)
        {
            if(sceneNumber + 2 != 5)
                SceneManager.LoadScene(sceneNumber + 2);
            else
                SceneManager.LoadScene(0);

        }
        else if (oroTotal < oroMax)
        {
            SceneManager.LoadScene(sceneNumber + 1);

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PplayerMovement>() != null)
        {
            timer = Time.time;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        //if (other.GetComponent<PplayerMovement>() != null)
        //{
        //    if(Time.time-timer > 0.2f)
        //    {
        //        timer = Time.time;
        //        if(other.GetComponent<PplayerMovement>().oro > 0 && oroTotal < 0.95f)
        //        {
        //            other.GetComponent<PplayerMovement>().oro--;
        //            oroTotal += (float)1/ (float)oroMax;

        //            if(oroTotal >= 1)
        //                oroTotal = 1;

        //        }

        //    }
        
        //}
    }
}
