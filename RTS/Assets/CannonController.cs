using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    [SerializeField]
    public PplayerMovement myPlayer;

    public bool playerInside;
    public bool doItOnce;
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

}
