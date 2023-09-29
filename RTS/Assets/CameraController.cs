using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Camera maincam;
    public enum cameraPlacement
    {
        NORTH, WEST, EAST, SOUTH
    };

    private cameraPlacement cameraDirection;
    // Start is called before the first frame update
    void Start()
    {
        maincam = FindObjectOfType<Camera>();
        cameraDirection = cameraPlacement.NORTH;
    }

    // Update is called once per frame
    void Update()
    {
         if(Input.GetKeyDown(KeyCode.LeftArrow)) {

            if(cameraDirection == cameraPlacement.SOUTH)
            {
                cameraDirection = cameraPlacement.NORTH;

            }
            else
            {
                cameraDirection += 1;
                ChangeCameraPlacement(cameraDirection);
            }
            Debug.Log(cameraDirection);
         }   
    }


    public void ChangeCameraPlacement(cameraPlacement placement)
    {
        switch (placement)
        {
            case cameraPlacement.NORTH:
                //Camera points north up
                maincam.transform.position = new Vector3(0f, 15.1428566f, -15f);
                //maincam.transform.rotation = new Quaternion(45, 0, 0,0);
                break; 
            case cameraPlacement.SOUTH:
                //Camera points south down
                maincam.transform.position = new Vector3(0f, 15.1428566f, -15f);
                //maincam.transform.rotation = new Quaternion(45, 0, 0, 0);
                break; 
            case cameraPlacement.EAST:
                //Camera points east right
                maincam.transform.position = new Vector3(0f, 15.1428566f, -15f);
                //maincam.transform.rotation = new Quaternion(45, 0, 0, 0);
                break; 
            case cameraPlacement.WEST:
                //Camera points west left
                maincam.transform.position = new Vector3(0f, 15.1428566f, -15f);
                //maincam.transform.rotation = new Quaternion(45, 0, 0, 0);
                break;
            default:

                break;
        }
    }
}
