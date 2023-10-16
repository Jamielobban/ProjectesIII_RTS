using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class housesController : MonoBehaviour
{
    GameObject[] houses = new GameObject[6];
    public int houseNumber;
    // Start is called before the first frame update
    void Start()
    {
        houseNumber = this.transform.childCount;
        for (int i = 0; i < houseNumber; i++)
        {
        houses[i] = this.transform.GetChild(i).GetChild(0).gameObject;
        }
       
        StartCoroutine(pedir(Random.RandomRange(5, 10)));
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
        for (int i = 0; i < houseNumber; i++)
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
            //Debug.Log(result);
            //Debug.Log(a[result]);

        }


        StartCoroutine(pedir(Random.RandomRange(15, 20)));
    }
}
