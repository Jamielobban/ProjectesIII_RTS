using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PplayerMovement : MonoBehaviour
{
    public float movementSpeed;
    public float fuerza;
    mainCameraController camera;
    Vector3 look = new Vector3();
    // Start is called before the first frame update
    void Start()
    {
        camera = FindObjectOfType<mainCameraController>();
    }
    //UnityEditor.TransformWorldPlacementJSON:{"position":{"x":11.489999771118164,"y":7.571428298950195,"z":-17.75},"rotation":{"x":0.3826834261417389,"y":0.0,"z":0.0,"w":0.9238795638084412},"scale":{"x":1.0,"y":1.0,"z":1.0}}

    // Update is called once per frame
    void Update()
    {

        look = this.transform.position;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            this.GetComponent<Rigidbody>().AddForce(new Vector3(0, fuerza, 0), ForceMode.Impulse);
        }
        Vector3 vel = new Vector3();
        if (Input.GetKey(KeyCode.W))
        {
            if (GameObject.ReferenceEquals(camera.currentCamera, camera.cameras[0]))
                vel += this.transform.forward * movementSpeed * Time.deltaTime;
            else
                vel += camera.forward * movementSpeed * Time.deltaTime;

            look += camera.forward;


        }
        if (Input.GetKey(KeyCode.S))
        {
            if (GameObject.ReferenceEquals(camera.currentCamera, camera.cameras[0]))
                vel -= this.transform.forward * movementSpeed * Time.deltaTime;
            else
                vel -= camera.forward * movementSpeed * Time.deltaTime;
            look -= camera.forward;

        }
        if (Input.GetKey(KeyCode.A))
        {
            vel -= camera.right * movementSpeed * Time.deltaTime;
            this.transform.GetChild(0).rotation = new Quaternion(0, 90, 0, 0);
            look -= camera.right;

        }
        if (Input.GetKey(KeyCode.D))
        {
            vel += camera.right * movementSpeed * Time.deltaTime;
            this.transform.GetChild(0).rotation = new Quaternion(0, -90, 0, 0);
            look += camera.right;

        }
        transform.GetChild(0).LookAt(look);

        this.transform.position += vel.normalized * Time.deltaTime * movementSpeed;

    }
}
