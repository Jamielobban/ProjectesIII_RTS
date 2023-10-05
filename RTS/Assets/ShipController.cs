using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShipController : MonoBehaviour
{

    public Transform goToSpot;

    public Collider toHit;

    public Transform startPoint;
    // Start is called before the first frame update
    void Start()
    {
        startPoint = this.gameObject.transform;
        this.gameObject.transform.DOMove(new Vector3(startPoint.transform.position.x + 39.59f,startPoint.transform.position.y,startPoint.transform.position.z),3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
