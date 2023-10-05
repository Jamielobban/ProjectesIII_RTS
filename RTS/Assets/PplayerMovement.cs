using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using System;

public class PplayerMovement : MonoBehaviour
{
    public float movementSpeed;
    public float fuerza;
    mainCameraController camera;
    Vector3 look = new Vector3();
    public TMP_Text text;
    private Animator myAnim;

    bool isRunning = false;
    bool isDown = false;
    bool isInInteractionRange = false;
    public bool canMove = true;
    public int oro = 0;

    public bool isOnLeftSide = false;
    public bool isOnRightSide = false;

    public bool controllingCannon = false;
    // Start is called before the first frame update
    void Start()
    {
        camera = FindObjectOfType<mainCameraController>();
        myAnim = transform.GetChild(0).GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        text.text = oro.ToString();

        if (!controllingCannon)
        {
            HandleMovement();
        }
        else
        {
            if (isOnLeftSide)
            {
                HandleCannonMovementLeft();

            }
            else if (isOnRightSide)
            {
                HandleCannonMovementRight();
            }
        }


        if (isRunning)
        {
            myAnim.SetBool("isRunning", true);
        }
        else
        {
            myAnim.SetBool("isRunning", false);
        }

    }

    private void HandleCannonMovementRight()
    {

        look = this.transform.position;
        Vector3 vel = new Vector3();


        if (Input.GetKey(KeyCode.A) && canMove)
        {
            vel -= camera.right * movementSpeed * Time.deltaTime;
            this.transform.GetChild(0).rotation = new Quaternion(0, 90, 0, 0);
            look -= camera.right;
            isRunning = true;
        }


        this.GetComponent<Rigidbody>().AddForce((vel.normalized * movementSpeed * Time.deltaTime), ForceMode.Force);

        this.GetComponent<Rigidbody>().velocity = new Vector3(0, this.GetComponent<Rigidbody>().velocity.y, 0);

        transform.GetChild(0).LookAt(look);

        if (((Input.GetKey(KeyCode.W)) || (Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.S)) || (Input.GetKey(KeyCode.D))) == false)
        {
            isRunning = false;
        }
    }

    private void HandleCannonMovementLeft(){

        look = this.transform.position;
        Vector3 vel = new Vector3();

        if (Input.GetKey(KeyCode.D) && canMove)
        {
            vel += camera.right * movementSpeed * Time.deltaTime;
            this.transform.GetChild(0).rotation = new Quaternion(0, -90, 0, 0);
            look += camera.right;
            isRunning = true;

        }

        this.GetComponent<Rigidbody>().AddForce((vel.normalized * movementSpeed * Time.deltaTime), ForceMode.Force);

        this.GetComponent<Rigidbody>().velocity = new Vector3(0, this.GetComponent<Rigidbody>().velocity.y, 0);

        transform.GetChild(0).LookAt(look);

        if (((Input.GetKey(KeyCode.W)) || (Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.S)) || (Input.GetKey(KeyCode.D))) == false)
        {
            isRunning = false;
        }
    }
      
public void HandleMovement()
    {
        look = this.transform.position;

  

        Vector3 vel = new Vector3();

        if (Input.GetKey(KeyCode.W)&&canMove)
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


        if (Input.GetKey(KeyCode.S)&&canMove)
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

        if (Input.GetKey(KeyCode.A)&&canMove)
        {
            vel -= camera.right * movementSpeed * Time.deltaTime;
            this.transform.GetChild(0).rotation = new Quaternion(0, 90, 0, 0);
            look -= camera.right;
            isRunning = true;
        }

        if (Input.GetKey(KeyCode.D)&&canMove)
        {
            vel += camera.right * movementSpeed * Time.deltaTime;
            this.transform.GetChild(0).rotation = new Quaternion(0, -90, 0, 0);
            look += camera.right;
            isRunning = true;

        }


        this.GetComponent<Rigidbody>().AddForce((vel.normalized* movementSpeed * Time.deltaTime),ForceMode.Force);

        this.GetComponent<Rigidbody>().velocity = new Vector3(0, this.GetComponent<Rigidbody>().velocity.y, 0) ;

        transform.GetChild(0).LookAt(look);

        if (((Input.GetKey(KeyCode.W)) || (Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.S)) || (Input.GetKey(KeyCode.D))) == false)
        {
            isRunning = false;
        }

        

    }
    public void Recoger()
    {
        isRunning = false;
        myAnim.SetBool("isPickingUp", true);
        canMove = false;
        StartCoroutine(setAnimationBoolToFalse(0.5f, "isPickingUp"));
    }

    public void Farmear()
    {
        isRunning = false;
        //myAnim.SetBool("isPickingUp", true);
        canMove = false;
        //StartCoroutine(setAnimationBoolToFalse(1.10f, "isPickingUp"));
    }
    public void Salir()
    {
        isRunning = false;
        //myAnim.SetBool("isPickingUp", true);
        canMove = true;
        //StartCoroutine(setAnimationBoolToFalse(1.10f, "isPickingUp"));
    }

    private IEnumerator setAnimationBoolToFalse(float waitTime, string name)
    {
        yield return new WaitForSeconds(waitTime);
        myAnim.SetBool(name, false);
        yield return new WaitForSeconds(0.2f);

        canMove = true;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("LeftSideCannon"))
        {
            isOnLeftSide = true;
            isOnRightSide = false;
        }
        if (other.gameObject.CompareTag("RightSideCannon"))
        {
            isOnRightSide = true;
            isOnLeftSide = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("LeftSideCannon"))
        {
            isOnLeftSide = false;
        }
        if (other.gameObject.CompareTag("RightSideCannon"))
        {
            isOnRightSide = false;
        }
    }


}
