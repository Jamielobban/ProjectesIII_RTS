using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    [SerializeField]
    public PplayerMovement myPlayer;

    public bool playerControlled;
    public bool doItOnce;

    [SerializeField]
    private GameObject cannonball;

    [SerializeField]
    private Transform firingPoint;

    public float firingSpeed = 2000.0f;
    // Start is called before the first frame update
    void Start()
    {
        myPlayer  = FindObjectOfType<PplayerMovement>();
    }

    public void ChangePlayerSpeedTrasnform(float playerSpeed,Transform parent)
    {
        myPlayer.movementSpeed = playerSpeed;
        this.gameObject.transform.SetParent(parent.transform);
    }


    private void Update()
    {
        if(playerControlled)
        {
            if(Input.GetMouseButtonDown(0))
            {
                SpawnCannonShot();
            }
        }
    }

    void SpawnCannonShot()
    {
        cannonball = Instantiate(cannonball, firingPoint.transform.position, Quaternion.identity);
        cannonball.GetComponent<Rigidbody>().AddForce(new Vector3(0f, cannonball.GetComponent<Rigidbody>().velocity.y, 40.0f), ForceMode.Impulse);
    }

}
