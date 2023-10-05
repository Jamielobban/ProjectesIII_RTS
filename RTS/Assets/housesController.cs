using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class housesController : MonoBehaviour
{
    GameObject[] houses = new GameObject[6];

    // Start is called before the first frame update
    void Start()
    {
        houses[0] = this.transform.GetChild(0).GetChild(0).gameObject;
        houses[1] = this.transform.GetChild(1).GetChild(0).gameObject;
        houses[2] = this.transform.GetChild(2).GetChild(0).gameObject;
        houses[3] = this.transform.GetChild(3).GetChild(0).gameObject;
        houses[4] = this.transform.GetChild(4).GetChild(0).gameObject;
        houses[5] = this.transform.GetChild(5).GetChild(0).gameObject;
        StartCoroutine(pedir(Random.RandomRange(10, 20)));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator pedir(float time)
    {
        yield return new WaitForSeconds(time);
        int total = 0;
        int[] a = new int[0];
        for (int i = 0; i < houses.Length; i++)
        {
            if (houses[i].GetComponent<house>().GetState() == 2)
            {
                total++;
                int[] e = new int[total-1];
                e = a;
                a = new int[total];
                for (int j = 0;j <e.Length;j++)
                    a[j] = e[j];
                a[a.Length - 1] = i;

            }
        }
        if (total != 0) 
        {
            int result = Random.Range(0, total-1);
            houses[a[result]].GetComponent<house>().StartPedir();
            Debug.Log(result);
            Debug.Log(a[result]);

        }


        StartCoroutine(pedir(Random.RandomRange(25, 40)));
    }
}
