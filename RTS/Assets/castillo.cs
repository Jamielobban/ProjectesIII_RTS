using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class castillo : MonoBehaviour
{
    [SerializeField]
    public Slider oro;
    public Slider tiempo;

    public float rellenarBarra = 1;
    public float velocidadVaciar = 0.01f;
    float timer = 0;
    float oroTotal = 0;
    float oroMax = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rellenarBarra -= velocidadVaciar * Time.deltaTime;
        oro.value = oroTotal;
        tiempo.value = rellenarBarra;
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
        if (other.GetComponent<PplayerMovement>() != null)
        {
            if(Time.time-timer > 0.2f)
            {
                timer = Time.time;
                if(other.GetComponent<PplayerMovement>().oro > 0 && oroTotal < 0.95f)
                {
                    other.GetComponent<PplayerMovement>().oro--;
                    oroTotal += (float)1/ (float)oroMax;

                    if(oroTotal >= 1)
                        oroTotal = 1;

                }

            }
        
        }
    }
}
