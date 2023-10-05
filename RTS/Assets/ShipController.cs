using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShipController : MonoBehaviour
{

    public Transform goToSpot;

    public Collider toHit;

    public Transform startPoint;
    GameObject casas;
    // Start is called before the first frame update
    void Start()
    {
        casas = GameObject.FindGameObjectWithTag("Casas");
        startPoint = this.gameObject.transform;
        this.gameObject.transform.DOMove(new Vector3(startPoint.transform.position.x + 39.59f,startPoint.transform.position.y,startPoint.transform.position.z),3.0f);
        StartCoroutine(quemar(Random.RandomRange(1, 2)));

    }
    private IEnumerator quemar(float time)
    {
        yield return new WaitForSeconds(time);
        int total = 0;
        int[] a = new int[0];
        for (int i = 0; i < casas.transform.childCount; i++)
        {
            if (casas.transform.GetChild(i).GetChild(0).GetComponent<house>().GetState() != 0)
            {
                total++;
                int[] e = new int[total - 1];
                e = a;
                a = new int[total];
                for (int j = 0; j < e.Length; j++)
                    a[j] = e[j];
                a[a.Length - 1] = i;

            }
        }
        if (total != 0)
        {
            int result = Random.Range(0, total - 1);
            casas.transform.GetChild(a[result]).GetChild(0).GetComponent<house>().PrenderFuego();


        }


        StartCoroutine(quemar(Random.RandomRange(15, 25)));
    }
    // Update is called once per frame
    void Update()
    {
        
    }

}
