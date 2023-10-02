using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class PplayerMovement : MonoBehaviour
{
    public float movementSpeed;
    public float fuerza;
    mainCameraController camera;
    Vector3 look = new Vector3();

    private Animator myAnim;

    bool isRunning = false;
    bool isDown = false;
    bool isInInteractionRange = false;
    // Start is called before the first frame update
    void Start()
    {
        camera = FindObjectOfType<mainCameraController>();
        myAnim = transform.GetChild(0).GetComponent<Animator>();
    }
    

    // Update is called once per frame
    void Update()
    {
        HandleMovement(); 

        if (isRunning)
        {
            myAnim.SetBool("isRunning", true);
        }
        else
        {
            myAnim.SetBool("isRunning", false);
        }

    }


    public void HandleMovement()
    {
        look = this.transform.position;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.GetComponent<Rigidbody>().AddForce(new Vector3(0, fuerza, 0), ForceMode.Impulse);
        }

        Vector3 vel = new Vector3();

        if (Input.GetKey(KeyCode.W))
        {
            if (GameObject.ReferenceEquals(camera.currentCamera, camera.cameras[0]))
            {
                vel += this.transform.forward * movementSpeed * Time.deltaTime;
                isRunning = true;
            }
            else
            {
                vel += camera.forward * movementSpeed * Time.deltaTime;

            }
            look += camera.forward;
        }


        if (Input.GetKey(KeyCode.S))
        {
            if (GameObject.ReferenceEquals(camera.currentCamera, camera.cameras[0]))
            {
                isRunning = true;
                vel -= this.transform.forward * movementSpeed * Time.deltaTime;
            }
            else
            {
                vel -= camera.forward * movementSpeed * Time.deltaTime;

            }
            look -= camera.forward;

        }

        if (Input.GetKey(KeyCode.A))
        {
            vel -= camera.right * movementSpeed * Time.deltaTime;
            this.transform.GetChild(0).rotation = new Quaternion(0, 90, 0, 0);
            look -= camera.right;
            isRunning = true;
        }

        if (Input.GetKey(KeyCode.D))
        {
            vel += camera.right * movementSpeed * Time.deltaTime;
            this.transform.GetChild(0).rotation = new Quaternion(0, -90, 0, 0);
            look += camera.right;
            isRunning = true;

        }

        if((Input.GetKey(KeyCode.E))/* && isInInteractionRange*/)
        {
            myAnim.SetBool("isPickingUp", true);
            StartCoroutine(setAnimationBoolToFalse(1.10f, "isPickingUp"));
        }

        this.transform.position += vel.normalized * Time.deltaTime * movementSpeed;

        transform.GetChild(0).LookAt(look);

        if (((Input.GetKey(KeyCode.W)) || (Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.S)) || (Input.GetKey(KeyCode.D))) == false)
        {
            isRunning = false;
        }

        

    }

    private IEnumerator setAnimationBoolToFalse(float waitTime, string name)
    {
        yield return new WaitForSeconds(waitTime);
        myAnim.SetBool(name, false);
    }
}
