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
    //UnityEditor.TransformWorldPlacementJSON:{"position":{"x":11.489999771118164,"y":7.571428298950195,"z":-17.75},"rotation":{"x":0.3826834261417389,"y":0.0,"z":0.0,"w":0.9238795638084412},"scale":{"x":1.0,"y":1.0,"z":1.0}}

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
