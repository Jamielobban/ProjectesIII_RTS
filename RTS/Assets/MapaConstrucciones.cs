using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapaConstrucciones : MonoBehaviour
{

    GameObject[][] pos = new GameObject[28][];
    GameObject[][] Cons = new GameObject[28][];
    public bool construir;
    public player player;
    GameObject visualizacion;
    public Material rojo;
    public Material verde;
    bool color;
    bool crear;

    enum typeEdificio {archery,barracs,muralla1, muralla2, muralla3, muralla4,torre };
    typeEdificio edificio;

    int tipo;
    public GameObject[] tiposEdificio;
    public GameObject[] edificioFinal;

    // Start is called before the first frame update
    void Start()
    {
        tipo = 0;
        edificio = typeEdificio.archery;
        color = false;
        visualizacion = null;
        player = FindObjectOfType<player>();
        construir = false;
        for (int i = 0; i <28; i++)
        {
            pos[i] = new GameObject[37];
            Cons[i] = new GameObject[37];

        }

        for (int i = 0; i<28; i++)
        {
            for (int e = 0; e < 37; e++)
            {
                pos[i][e] = this.transform.GetChild((i * 37) + e).gameObject;
            }
        }

    }
    public void destroy(Vector2 pos2, GameObject a)
    {
        Vector3 pos3 = pos[(int)pos2.x][(int)pos2.y].transform.position;
        Destroy(pos[(int)pos2.x][(int)pos2.y]);
        pos[(int)pos2.x][(int)pos2.y] = Instantiate(a, this.transform);
        pos[(int)pos2.x][(int)pos2.y].transform.position = pos3;
    }

    public void Construir(Vector2 pos2, GameObject a)
    {
        if(Cons[(int)pos2.x][(int)pos2.y] == null)
        {
            Cons[(int)pos2.x][(int)pos2.y] = Instantiate(a, this.transform);
            Cons[(int)pos2.x][(int)pos2.y].transform.position = pos[(int)pos2.x][(int)pos2.y].transform.position + new Vector3(0,1,0);
        }
    }
    void Visualizar(Vector2 pos2)
    {
        if (Cons[(int)pos2.x][(int)pos2.y] == null)
        {
            visualizacion.transform.position = pos[(int)pos2.x][(int)pos2.y].transform.position + new Vector3(0, 1, 0);

            if(pos[(int)pos2.x][(int)pos2.y].CompareTag("camino") && !color)
            {

                CambiarColor(rojo);

                color = true;
            }
            else if(color && !pos[(int)pos2.x][(int)pos2.y].CompareTag("camino"))
            {
                CambiarColor(verde);

                color = false;
            }

            if(Input.GetKeyDown(KeyCode.Q))
            {
                visualizacion.transform.Rotate(new Vector3(0, 60, 0));
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                visualizacion.transform.Rotate(new Vector3(0, -60, 0));

            }
        }
    }
    void CambiarColor(Material a)
    {
        Material[] nuevosMateriales = new Material[visualizacion.GetComponent<Renderer>().materials.Length];
        for (int i = 0; i < nuevosMateriales.Length; i++)
        {
            nuevosMateriales[i] = a;
        }
        visualizacion.GetComponent<Renderer>().materials = nuevosMateriales;
    }
    void EstablecerModelo(Vector2 pos2, GameObject modelo, Transform pos)
    {
        Cons[(int)pos2.x][(int)pos2.y] = Instantiate(modelo, this.transform);
        Cons[(int)pos2.x][(int)pos2.y].transform.position = pos.position;
        Cons[(int)pos2.x][(int)pos2.y].transform.rotation = pos.rotation;
    }
    void Establecer(Vector2 pos2)
    {

            Transform pos = null;
            if (visualizacion != null)
            {
                pos = visualizacion.transform;
                Destroy(visualizacion);
            }

            switch (edificio)
            {
            case typeEdificio.archery:

                    EstablecerModelo(pos2, edificioFinal[0], pos);
                break;
            case typeEdificio.barracs:

                    EstablecerModelo(pos2, edificioFinal[1], pos);
                break;
            case typeEdificio.muralla1:

                EstablecerModelo(pos2, edificioFinal[2], pos);
                break;
            case typeEdificio.muralla2:

                EstablecerModelo(pos2, edificioFinal[3], pos);
                break;
            case typeEdificio.muralla3:

                EstablecerModelo(pos2, edificioFinal[4], pos);
                break;
            case typeEdificio.muralla4:

                EstablecerModelo(pos2, edificioFinal[5], pos);
                break;
            case typeEdificio.torre:

                EstablecerModelo(pos2, edificioFinal[6], pos);
                break;
        }
        
    }
    void SeeColor(Vector2 pos2)
    {
        if (pos[(int)pos2.x][(int)pos2.y].CompareTag("camino"))
        {

            CambiarColor(rojo);

            color = true;
        }
        else
        {
            CambiarColor(verde);

            color = false;
        }
    }

    void crearVisualizacion(GameObject modelo)
    {
        if (!crear)
        {
            Quaternion rot = new Quaternion();
            if (visualizacion != null)
            {
                rot = visualizacion.transform.rotation;

                Destroy(visualizacion);
            }
            visualizacion = Instantiate(modelo, this.transform);
            visualizacion.transform.rotation = rot;
            SeeColor(player.GetPos());

            crear = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!construir)
            {
                construir = true;
            }
            else
            {
                if (!pos[(int)player.GetPos().x][(int)player.GetPos().y].CompareTag("camino"))
                {
                    construir = false;
                    Establecer(player.GetPos());
                    crear = false;
                }
            }
        }

        if (construir)
        {

            switch(edificio)
            {
                case typeEdificio.archery:

                    crearVisualizacion(tiposEdificio[0]);
                    break;
                case typeEdificio.barracs:

                    crearVisualizacion(tiposEdificio[1]);
                    break;
                case typeEdificio.muralla1:

                    crearVisualizacion(tiposEdificio[2]);
                    break;
                case typeEdificio.muralla2:

                    crearVisualizacion(tiposEdificio[3]);
                    break;
                case typeEdificio.muralla3:

                    crearVisualizacion(tiposEdificio[4]);
                    break;
                case typeEdificio.muralla4:

                    crearVisualizacion(tiposEdificio[5]);
                    break;
                case typeEdificio.torre:

                    crearVisualizacion(tiposEdificio[6]);
                    break;
            }

            if((int)Input.mouseScrollDelta.y != 0)
            {
                int a = (((int)edificio) + (int)Input.mouseScrollDelta.y) % tiposEdificio.Length;
                if (a < 0)
                    a = tiposEdificio.Length + a;

                edificio = (typeEdificio)a;
                crear = false;
            }

        

            Visualizar(player.GetPos());
            if (Input.GetMouseButtonDown(1))
            {
                crear = false;

                construir = false;
                if (visualizacion != null)
                {
                    Destroy(visualizacion);
                }
            }

        }
    }
}
