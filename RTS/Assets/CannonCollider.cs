using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CannonCollider : MonoBehaviour
{

    [SerializeField]
    private BoxCollider collider;

    [SerializeField]
    private CannonController parentCannon;

    [SerializeField]
    public GameObject cannons;

    bool holdingOn = false;
    // Start is called before the first frame update
    void Start()
    {
        parentCannon = transform.GetComponentInParent<CannonController>();
        cannons = GameObject.FindGameObjectWithTag("Cannons");
        collider = GetComponent<BoxCollider>();    
    }


    private void Update()
    {
        if (Input.GetKey(KeyCode.R) && holdingOn)
        {
            DisableCollider(0.5f);
            parentCannon.ChangePlayerSpeedTrasnform(5, cannons.transform);
            holdingOn = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) {
            holdingOn = true;
            parentCannon.ChangePlayerSpeedTrasnform(2,parentCannon.myPlayer.transform);
        }
    }


    private IEnumerator DisableCollider(float waitTime)
    {
        collider.enabled = false;
        yield return new WaitForSeconds(waitTime);
        collider.enabled = true;
    }

}
