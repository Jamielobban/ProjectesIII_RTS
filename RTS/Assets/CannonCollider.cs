using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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


    [SerializeField]
    private PplayerMovement myPlayer;
    // Start is called before the first frame update
    void Start()
    {
        parentCannon = transform.GetComponentInParent<CannonController>();
        cannons = GameObject.FindGameObjectWithTag("Cannons");
        collider = GetComponent<BoxCollider>();    
        myPlayer = FindObjectOfType<PplayerMovement>();
    }


    private void Update()
    {
        if (Input.GetKey(KeyCode.R) && holdingOn)
        {
            //collider.enabled = false;
            StartCoroutine(DisableCollider(1f));
            parentCannon.ChangePlayerSpeedTrasnform(30, cannons.transform);
            holdingOn = false;
            myPlayer.controllingCannon = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) {
            
            holdingOn = true;
            parentCannon.playerControlled = true;
            ///parentCannon.transform.GetComponentInParent<PplayerMovement>().controllingCannon = true;
            myPlayer.controllingCannon = true;
            parentCannon.ChangePlayerSpeedTrasnform(15,parentCannon.myPlayer.transform);
        }
    }


    private IEnumerator DisableCollider(float waitTime)
    {
        collider.enabled = false;
        yield return new WaitForSeconds(waitTime);
        collider.enabled = true;
    }



}
