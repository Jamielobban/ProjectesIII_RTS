using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialController : MonoBehaviour
{
    enum Objects {Madera,Piedra,Agua,Trigo,Comida,Nada}
    Objects[] inventario = { Objects.Nada, Objects.Nada, Objects.Nada };
    public Texture[] texturas;
    public GameObject[] textura;

    int puesto = 0;
    // Start is called before the first frame update
    void Start()
    {
        textura[0].GetComponent<Animator>().SetTrigger("Abrir");
        textura[1].GetComponent<Animator>().SetTrigger("Cerrar");
        textura[2].GetComponent<Animator>().SetTrigger("Cerrar");
        textura[0].transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        textura[1].transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        textura[2].transform.GetChild(0).GetChild(0).gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            if(puesto!= 0)
            {
            textura[puesto].GetComponent<Animator>().SetTrigger("Cerrar");

            puesto = 0;
            textura[puesto].GetComponent<Animator>().SetTrigger("Abrir");
            }


        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (puesto != 1)
            {
                textura[puesto].GetComponent<Animator>().SetTrigger("Cerrar");

                puesto = 1;
                textura[puesto].GetComponent<Animator>().SetTrigger("Abrir");
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (puesto != 2)
            {
                textura[puesto].GetComponent<Animator>().SetTrigger("Cerrar");

                puesto = 2;
                textura[puesto].GetComponent<Animator>().SetTrigger("Abrir");
            }
        }

    }
    public int GetCurrentState()
    {

        return (int)inventario[puesto];
    }
    public void SetTexture(int texture)
    {
        if(texture != 5)
        {
            this.GetComponent<PplayerMovement>().movementSpeed -= 15;
            textura[puesto].transform.GetChild(0).GetChild(0).gameObject.SetActive(true);

            inventario[puesto] = (Objects)texture;
            textura[puesto].transform.GetChild(0).GetChild(0).GetComponent<RawImage>().texture = texturas[texture];
        }
        else
        {
            this.GetComponent<PplayerMovement>().movementSpeed += 15;
            textura[puesto].transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
            inventario[puesto] = (Objects)texture;

        }

    }

}
