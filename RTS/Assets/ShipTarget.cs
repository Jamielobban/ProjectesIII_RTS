using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipTarget : MonoBehaviour
{

    private BoxCollider myBC;
    private GameObject cannonBall;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.localPosition = new Vector3(8.64f, 0f, Random.Range(-11.8f, 11.12f));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cannonball"))
        {
            cannonBall = other.gameObject;
            Destroy(cannonBall);
            Destroy(this.gameObject.transform.parent.gameObject);
        }
    }
}
