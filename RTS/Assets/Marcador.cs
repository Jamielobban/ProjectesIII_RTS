using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marcador : MonoBehaviour
{
    MaterialController player;
    public int type;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<MaterialController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetCurrentState() == type)
            this.transform.GetChild(0).gameObject.SetActive(true);
        else
            this.transform.GetChild(0).gameObject.SetActive(false);

    }
}
