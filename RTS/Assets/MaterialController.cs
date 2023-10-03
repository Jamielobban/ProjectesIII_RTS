using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialController : MonoBehaviour
{
    enum Objects {Madera,Piedra,Agua,Trigo,Comida,Nada}
    Objects[] inventario = { Objects.Nada, Objects.Nada, Objects.Nada };
    public Texture[] texturas;
    public RawImage[] textura;

    int puesto = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            puesto = 0;

        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            puesto = 1;

        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            puesto = 2;

        }

    }

    public void SetTexture(int texture)
    {
        inventario[puesto] = (Objects)texture;
        textura[puesto].texture = texturas[texture];
    }

}
