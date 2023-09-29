using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PplayerMovement : MonoBehaviour
{
    public float movementSpeed;
    public float fuerza;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        if(Input.GetKeyDown(KeyCode.Space))
        {
            this.GetComponent<Rigidbody>().AddForce(new Vector3(0, fuerza, 0), ForceMode.Impulse);
        }
        Vector3 vel = new Vector3();
        if (Input.GetKey(KeyCode.W))
        {

            vel += this.transform.forward * movementSpeed * Time.deltaTime;
            this.transform.GetChild(0).rotation = new Quaternion(0, 0, 0, 0);

        }
        if (Input.GetKey(KeyCode.S))
        {
            vel -= this.transform.forward * movementSpeed * Time.deltaTime;
            this.transform.GetChild(0).rotation = new Quaternion(0, 180, 0, 0);

        }
        if (Input.GetKey(KeyCode.A))
        {
            vel -= this.transform.right * movementSpeed * Time.deltaTime;
            this.transform.GetChild(0).rotation = new Quaternion(0, 90, 0, 0);

        }
        if (Input.GetKey(KeyCode.D))
        {
            vel += this.transform.right * movementSpeed * Time.deltaTime;
            this.transform.GetChild(0).rotation = new Quaternion(0, -90, 0, 0);

        }
        this.transform.position += vel.normalized * Time.deltaTime * movementSpeed;

    }
}
