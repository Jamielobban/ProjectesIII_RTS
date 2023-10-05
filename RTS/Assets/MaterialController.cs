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
    bool changing = false;
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
        if ((int)Input.mouseScrollDelta.y != 0&&!changing)
        {
            changing = true;
            Invoke("Change", 0.25f);
            textura[puesto].GetComponent<Animator>().SetTrigger("Cerrar");

            puesto = (puesto + (int)Input.mouseScrollDelta.y)%3;
            if(puesto < 0)
            {
                puesto = 3 + puesto;
            }
            textura[puesto].GetComponent<Animator>().SetTrigger("Abrir");
        }
       

    }
    void Change()
    {
        changing = false;

    }
    public int GetCurrentState()
    {

        return (int)inventario[puesto];
    }
    public void SetTexture(int texture)
    {
        if(texture != 5)
        {
            this.GetComponent<PplayerMovement>().movementSpeed -= 3000;
            textura[puesto].transform.GetChild(0).GetChild(0).gameObject.SetActive(true);

            inventario[puesto] = (Objects)texture;
            textura[puesto].transform.GetChild(0).GetChild(0).GetComponent<RawImage>().texture = texturas[texture];
        }
        else
        {
            this.GetComponent<PplayerMovement>().movementSpeed += 3000;
            textura[puesto].transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
            inventario[puesto] = (Objects)texture;

        }

    }

}
