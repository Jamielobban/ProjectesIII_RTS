using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapaConstrucciones : MonoBehaviour
{
    
    GameObject[][] pos = new GameObject[28][];
    GameObject[][] Cons = new GameObject[28][];

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i <28; i++)
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
    // Update is called once per frame
    void Update()
    {
        
    }
}
