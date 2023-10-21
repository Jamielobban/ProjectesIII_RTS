using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void recoger()
    {
        oroTotal += 5;
        if(oroTotal > oroMax)
        {
            oroTotal = oroMax;

        }
        oro.value = (float)((float)oroTotal / (float)oroMax);


    }
    // Update is called once per frame
    void Update()
    {
        rellenarBarra -= velocidadVaciar * Time.deltaTime;
        tiempo.value = rellenarBarra;

        if (rellenarBarra <= 0 && oroTotal == oroMax)
        {
            if(sceneNumber + 2 == 5)
                SceneManager.LoadScene(0);
            else 
                SceneManager.LoadScene(sceneNumber + 2);

        }
        else if (rellenarBarra <= 0 && oroTotal < oroMax)
        {
            SceneManager.LoadScene(sceneNumber+1);

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
