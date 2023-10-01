using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainCameraController : MonoBehaviour
{
    public GameObject[] cameras;
    public GameObject currentCamera;
    public Vector3 forward;
    public Vector3 right;

    // Start is called before the first frame update
    void Start()
    {
        currentCamera = cameras[0];
        forward = currentCamera.transform.forward;
        right = currentCamera.transform.right;

    }

    IEnumerator Change()
    {

            yield return new WaitForSeconds(0.25f);
        forward = currentCamera.transform.forward;
        right = currentCamera.transform.right;
    }
    // Update is called once per frame
    void Update()
    {
        if (GameObject.ReferenceEquals(currentCamera, cameras[1]))
        {
            currentCamera.transform.position = new Vector3(currentCamera.transform.position.x, currentCamera.transform.position.y,this.transform.position.z);

            if(currentCamera.transform.localPosition.z < -5)
            {
                currentCamera.transform.localPosition = new Vector3(currentCamera.transform.localPosition.x, currentCamera.transform.localPosition.y, -5);
            }
            else if (currentCamera.transform.localPosition.z > 5)
            {
                currentCamera.transform.localPosition = new Vector3(currentCamera.transform.localPosition.x, currentCamera.transform.localPosition.y, 5);
            }

        }
        else if (GameObject.ReferenceEquals(currentCamera, cameras[2]))
        {
            currentCamera.transform.position = new Vector3(this.transform.position.x, currentCamera.transform.position.y, currentCamera.transform.position.z);

            if (currentCamera.transform.localPosition.x < -13)
            {
                currentCamera.transform.localPosition = new Vector3(-13, currentCamera.transform.localPosition.y, currentCamera.transform.localPosition.z);
            }
            else if (currentCamera.transform.localPosition.x > 13)
            {
                currentCamera.transform.localPosition = new Vector3(13, currentCamera.transform.localPosition.y, currentCamera.transform.localPosition.z);
            }
        }
        else if (GameObject.ReferenceEquals(currentCamera, cameras[3]))
        {
            currentCamera.transform.position = new Vector3(currentCamera.transform.position.x, currentCamera.transform.position.y, this.transform.position.z);

            if (currentCamera.transform.localPosition.z < -5)
            {
                currentCamera.transform.localPosition = new Vector3(currentCamera.transform.localPosition.x, currentCamera.transform.localPosition.y, -5);
            }
            else if (currentCamera.transform.localPosition.z > 5)
            {
                currentCamera.transform.localPosition = new Vector3(currentCamera.transform.localPosition.x, currentCamera.transform.localPosition.y, 5);
            }
        }
        else if (GameObject.ReferenceEquals(currentCamera, cameras[4]))
        {
            currentCamera.transform.position = new Vector3(this.transform.position.x, currentCamera.transform.position.y, currentCamera.transform.position.z);

            if (currentCamera.transform.localPosition.x < -13)
            {
                currentCamera.transform.localPosition = new Vector3(-13, currentCamera.transform.localPosition.y, currentCamera.transform.localPosition.z);
            }
            else if (currentCamera.transform.localPosition.x > 13)
            {
                currentCamera.transform.localPosition = new Vector3(13, currentCamera.transform.localPosition.y, currentCamera.transform.localPosition.z);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "mainCamera")
        {
            if(!GameObject.ReferenceEquals(currentCamera, cameras[0]))
            {
                StartCoroutine(Change());
                currentCamera.SetActive(false);
                currentCamera = cameras[0];
                currentCamera.SetActive(true);
            }
        }
        else if(other.gameObject.name == "camera1")
        {
            if (!GameObject.ReferenceEquals(currentCamera, cameras[1]))
            {
                StartCoroutine(Change());

                currentCamera.SetActive(false);
                currentCamera = cameras[1];
                currentCamera.SetActive(true);
            }
        }
        else if(other.gameObject.name == "camera2")
        {
            if (!GameObject.ReferenceEquals(currentCamera, cameras[2]))
            {
                StartCoroutine(Change());

                currentCamera.SetActive(false);
                currentCamera = cameras[2];
                currentCamera.SetActive(true);
            }
        }
        else if (other.gameObject.name == "camera3")
        {
            if (!GameObject.ReferenceEquals(currentCamera, cameras[3]))
            {
                StartCoroutine(Change());

                currentCamera.SetActive(false);
                currentCamera = cameras[3];
                currentCamera.SetActive(true);
            }
        }
        else if (other.gameObject.name == "camera4")
        {
            if (!GameObject.ReferenceEquals(currentCamera, cameras[4]))
            {
                StartCoroutine(Change());

                currentCamera.SetActive(false);
                currentCamera = cameras[4];
                currentCamera.SetActive(true);
            }
        }
    }
}
