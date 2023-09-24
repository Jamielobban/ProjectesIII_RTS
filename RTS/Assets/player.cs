using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public Transform[] pos;
    int tropas;
    public float aa;
    public MapaConstrucciones mapa;
    public GameObject camino;
    public GameObject edificio;

    // Start is called before the first frame update
    void Start()
    {
        mapa = FindObjectOfType<MapaConstrucciones>();
        tropas = 0;
    }

    Vector2 GetPos()
    {
        float t = ((this.transform.position.z - 1.75f) % 1.75f);
        int x;
        x = 0;
        if(t < (1.75f/2f))
        {
            x = (int)((this.transform.position.z - 1.75f)/1.75f);
        }
        else
        {
            x = (int)((this.transform.position.z - 1.75f) / 1.75f);
            x++;
        }


              int dif = 0;
        if(x % 2 == 1)
        {
            dif = 1;
        }
        float e = ((this.transform.position.x - (16 + dif)) % 2);
        int z = 0;
  
        if(e < 1)
        {
            z = (int)((this.transform.position.x - (16+dif)) / 2);
        }
        else
        {
            z = (int)((this.transform.position.x - (16 + dif)) / 2);
            z++;
        }
        return new Vector2(x,z);


    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //RaycastHit hit;
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //if (Physics.Raycast(ray, out hit))
            //{
            //    // Check if hit.transform is door, 
            //    if (hit.transform.gameObject.CompareTag("personas"))
            //    {
            //        if(hit.transform.parent != null)
            //        {
            //            hit.transform.SetParent(null);
            //            tropas--;
            //            reordenar();
            //        }
            //        else
            //        {
            //            tropas++;
            //            añadir(hit.transform.gameObject);

            //        }

            //    }
            //}


            mapa.destroy(GetPos(), camino);
        }
        if (Input.GetMouseButtonDown(1))
        {
            mapa.Construir(GetPos(), edificio);
        }
            if (Input.GetKey(KeyCode.W))
        {

            this.transform.position += this.transform.forward * 5 * Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.position -= this.transform.forward * 5 * Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.position -= this.transform.right * 5 * Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.position += this.transform.right * 5 * Time.deltaTime;

        }
    }

    void reordenar()
    {
        Transform[] posi = new Transform[tropas];
        int aa = 0;

        for (int i = 0; i < pos.Length; i++)
        {
            if (pos[i].childCount != 0)
            {
                posi[aa] = pos[i].GetChild(0);
                aa++;
            }
        }

        reagrupar(posi);

    }
    void añadir(GameObject a)
    {
        Transform[] posi = new Transform[tropas];
        int aa = 0;
        posi[aa] = a.transform;
        aa++;
        for(int i = 0; i < pos.Length; i++)
        { 
            if (pos[i].childCount != 0)
            {
                posi[aa] = pos[i].GetChild(0);
                aa++;
            }
        }

        reagrupar(posi);
    }

    void reagrupar(Transform[] posi)
    {
        switch(posi.Length)
        {
            case 0:
                break;
            case 1:
                posi[0].transform.SetParent(pos[1]);
                posi[0].transform.localPosition = new Vector3(0,0,0);

                break;
            case 2:
                posi[0].transform.SetParent(pos[0]);
                posi[1].transform.SetParent(pos[2]);

                posi[0].transform.localPosition = new Vector3(0, 0, 0);
                posi[1].transform.localPosition = new Vector3(0, 0, 0);


                break;
            case 3:
                posi[0].transform.SetParent(pos[0]);
                posi[1].transform.SetParent(pos[1]);
                posi[2].transform.SetParent(pos[2]);

                posi[0].transform.localPosition = new Vector3(0, 0, 0);
                posi[1].transform.localPosition = new Vector3(0, 0, 0);
                posi[2].transform.localPosition = new Vector3(0, 0, 0);

                break;
            case 4:
                posi[0].transform.SetParent(pos[0]);
                posi[1].transform.SetParent(pos[1]);
                posi[2].transform.SetParent(pos[2]);
                posi[3].transform.SetParent(pos[4]);

                posi[0].transform.localPosition = new Vector3(0, 0, 0);
                posi[1].transform.localPosition = new Vector3(0, 0, 0);
                posi[2].transform.localPosition = new Vector3(0, 0, 0);
                posi[3].transform.localPosition = new Vector3(0, 0, 0);


                break;
            case 5:
                posi[0].transform.SetParent(pos[0]);
                posi[1].transform.SetParent(pos[1]);
                posi[2].transform.SetParent(pos[2]);
                posi[3].transform.SetParent(pos[3]);
                posi[4].transform.SetParent(pos[5]);

                posi[0].transform.localPosition = new Vector3(0, 0, 0);
                posi[1].transform.localPosition = new Vector3(0, 0, 0);
                posi[2].transform.localPosition = new Vector3(0, 0, 0);
                posi[3].transform.localPosition = new Vector3(0, 0, 0);
                posi[4].transform.localPosition = new Vector3(0, 0, 0);


                break;
            case 6:
                posi[0].transform.SetParent(pos[0]);
                posi[1].transform.SetParent(pos[1]);
                posi[2].transform.SetParent(pos[2]);
                posi[3].transform.SetParent(pos[3]);
                posi[4].transform.SetParent(pos[4]);
                posi[5].transform.SetParent(pos[5]);

                posi[0].transform.localPosition = new Vector3(0, 0, 0);
                posi[1].transform.localPosition = new Vector3(0, 0, 0);
                posi[2].transform.localPosition = new Vector3(0, 0, 0);
                posi[3].transform.localPosition = new Vector3(0, 0, 0);
                posi[4].transform.localPosition = new Vector3(0, 0, 0);
                posi[5].transform.localPosition = new Vector3(0, 0, 0);


                break;
            case 7:
                posi[0].transform.SetParent(pos[0]);
                posi[1].transform.SetParent(pos[1]);
                posi[2].transform.SetParent(pos[2]);
                posi[3].transform.SetParent(pos[3]);
                posi[4].transform.SetParent(pos[4]);
                posi[5].transform.SetParent(pos[5]);
                posi[6].transform.SetParent(pos[7]);

                posi[0].transform.localPosition = new Vector3(0, 0, 0);
                posi[1].transform.localPosition = new Vector3(0, 0, 0);
                posi[2].transform.localPosition = new Vector3(0, 0, 0);
                posi[3].transform.localPosition = new Vector3(0, 0, 0);
                posi[4].transform.localPosition = new Vector3(0, 0, 0);
                posi[5].transform.localPosition = new Vector3(0, 0, 0);
                posi[6].transform.localPosition = new Vector3(0, 0, 0);


                break;
            case 8:
                posi[0].transform.SetParent(pos[0]);
                posi[1].transform.SetParent(pos[1]);
                posi[2].transform.SetParent(pos[2]);
                posi[3].transform.SetParent(pos[3]);
                posi[4].transform.SetParent(pos[4]);
                posi[5].transform.SetParent(pos[5]);
                posi[6].transform.SetParent(pos[6]);
                posi[7].transform.SetParent(pos[8]);

                posi[0].transform.localPosition = new Vector3(0, 0, 0);
                posi[1].transform.localPosition = new Vector3(0, 0, 0);
                posi[2].transform.localPosition = new Vector3(0, 0, 0);
                posi[3].transform.localPosition = new Vector3(0, 0, 0);
                posi[4].transform.localPosition = new Vector3(0, 0, 0);
                posi[5].transform.localPosition = new Vector3(0, 0, 0);
                posi[6].transform.localPosition = new Vector3(0, 0, 0);
                posi[7].transform.localPosition = new Vector3(0, 0, 0);


                break;
            case 9:
                posi[0].transform.SetParent(pos[0]);
                posi[1].transform.SetParent(pos[1]);
                posi[2].transform.SetParent(pos[2]);
                posi[3].transform.SetParent(pos[3]);
                posi[4].transform.SetParent(pos[4]);
                posi[5].transform.SetParent(pos[5]);
                posi[6].transform.SetParent(pos[6]);
                posi[7].transform.SetParent(pos[7]);
                posi[8].transform.SetParent(pos[8]);

                posi[0].transform.localPosition = new Vector3(0, 0, 0);
                posi[1].transform.localPosition = new Vector3(0, 0, 0);
                posi[2].transform.localPosition = new Vector3(0, 0, 0);
                posi[3].transform.localPosition = new Vector3(0, 0, 0);
                posi[4].transform.localPosition = new Vector3(0, 0, 0);
                posi[5].transform.localPosition = new Vector3(0, 0, 0);
                posi[6].transform.localPosition = new Vector3(0, 0, 0);
                posi[7].transform.localPosition = new Vector3(0, 0, 0);
                posi[8].transform.localPosition = new Vector3(0, 0, 0);


                break;
        }
    }
}
