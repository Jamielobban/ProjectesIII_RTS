using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using System;
using LP.FDG.Units;
using UnityEngine.UI;
using UnityEngine.Rendering;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;
using Unity.VisualScripting.FullSerializer;
using System.Net.Mime;
using UnityEngine.AI;

public class PplayerMovement : MonoBehaviour
{
    public float movementSpeed;
    public float fuerza;
    mainCameraController camera;
    Vector3 look = new Vector3();
    //public TMP_Text text;
    private Animator myAnim;

    public Transform castilloPos;
    public UnitStatTypes.Base baseStats;
    public Transform aggroTarget;
    public Collider[] rangeColliders;
    public UnitStatDisplay aggroUnit;
    public float distance;

    private float atkCooldown;
    public Image healthBarAmount;

    public float currentHealth;
    public bool hasAggro = false;  
    bool isRunning = false;
    bool isDown = false;
    bool isInInteractionRange = false;
    public bool canMove = true;
    public int oro = 0;
    bool isInCombat = false;


    [SerializeField]
    private RuntimeAnimatorController combatController;
    [SerializeField]
    private RuntimeAnimatorController playingController;
    [SerializeField]
    private GameObject axeToEnable;


    public bool firstAttack;
    public bool secondAttack;
    public bool thirdAttack;
    public bool returnToIdle;
    
    // Start is called before the first frame update
    void Start()
    {

        camera = FindObjectOfType<mainCameraController>();
        myAnim = transform.GetChild(0).GetComponent<Animator>();
        this.transform.GetChild(0).rotation = new Quaternion();
    }

    bool acabado = false;
    // Update is called once per frame
    void Update()
    {
        //text.text = oro.ToString();
        if (acabado)
            return;
        HandleMovement();

        //if(isInCombat)
        //{
        //    myAnim.runtimeAnimatorController = combatController;
        //    //axeToEnable.SetActive(true);
        //}
        //else
        //{
        //    myAnim.runtimeAnimatorController = playingController;
        //    //axeToEnable.SetActive(false);
        //}
      
        if (isRunning)
        {
            myAnim.SetBool("isRunning", true);
        }
        else
        {
            myAnim.SetBool("isRunning", false);
        }

    }

    private void CombatAnimations()
    {

    }
    private void LateUpdate()
    {
        if (acabado)
            return;
        CheckForEnemyTargets();
        if(hasAggro)
        {
            atkCooldown -= Time.deltaTime;
            Attack();
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
        vel.y -= 9.81f * Time.deltaTime;

        this.GetComponent<CharacterController>().Move(vel.normalized * movementSpeed * Time.deltaTime);
        //this.GetComponent<Rigidbody>().AddForce((vel.normalized* movementSpeed * Time.deltaTime),ForceMode.Acceleration);

        //this.GetComponent<Rigidbody>().velocity = new Vector3(0, this.GetComponent<Rigidbody>().velocity.y, 0) ;

        transform.GetChild(0).LookAt(look);

        if (((Input.GetKey(KeyCode.W)) || (Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.S)) || (Input.GetKey(KeyCode.D))) == false)
        {
            isRunning = false;
        }

        if(!isInCombat && Input.GetKey(KeyCode.R))
        {
            axeToEnable.SetActive(true);
            isInCombat = true;
        }
        if(isInCombat && Input.GetKey(KeyCode.T))
        {
            axeToEnable.SetActive(false);
            isInCombat = false;
        }
    }
    public void Acabar()
    {
        acabado = true;
        this.gameObject.transform.GetChild(0).GetComponent<NavMeshAgent>().enabled = true;
        this.gameObject.GetComponent<MaterialController>().enabled = false;
        this.gameObject.transform.GetChild(0).GetComponent<NavMeshAgent>().SetDestination(castilloPos.position);
        this.transform.GetChild(0).GetComponent<Animator>().Play("Walking_A 0");
    }
    public bool HaLlegado()
    {

        return Vector3.Distance(this.transform.GetChild(0).position, castilloPos.position) < 2;
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

    private void CheckForEnemyTargets()
    {
        rangeColliders = Physics.OverlapSphere(transform.position, baseStats.aggroRange);
        //Debug.Log("Doing this");
        for (int i = 0; i < rangeColliders.Length; i++)
        {
            if (rangeColliders[i].gameObject.layer == UnitHandler.instance.eUnitLayer)
            {
                aggroTarget = rangeColliders[i].gameObject.transform;
                aggroUnit = aggroTarget.gameObject.GetComponentInChildren<UnitStatDisplay>();
                hasAggro = true;
                //Debug.Log("Enemy found");
                 distance = Vector3.Distance(aggroTarget.position, transform.position);
                //Debug.Log(distance);
                
                //Debug.Log(("ENEMY LOCATED"));
                break;
            }
            else
            {
                //if(myAnim.runtimeAnimatorController == combatController)
                //{
                //    firstAttack = false;
                //    secondAttack = false;
                //    thirdAttack = false;
                //    myAnim.SetBool("cancelAttack", true);
                //}

                distance = 0.0f;
                hasAggro = false;
                aggroTarget = null; 
                aggroUnit = null;
                myAnim.SetBool("cancelAttack", true);
                StartCoroutine(ReturnToWeaponIdle(1.5f, "cancelAttack"));
            }
        }
    }

    private void Attack()
    {
        if (atkCooldown <= 0 && distance <= baseStats.atkRange + 1)
        {
            //Debug.Log("In here");

            if (myAnim.runtimeAnimatorController == combatController && firstAttack == false)
            {
                myAnim.SetBool("isAttacking1", true);
                firstAttack = true;
            }
            else if (myAnim.runtimeAnimatorController == combatController && secondAttack == false && firstAttack == true)
            {
                myAnim.SetBool("isAttacking2", true);
                myAnim.SetBool("isAttacking1", false);
                secondAttack = true;
            }
            else if(myAnim.runtimeAnimatorController == combatController && thirdAttack == false && firstAttack == true && secondAttack == true)
            {
                myAnim.SetBool("isAttacking3", true);
                //Debug.Log("CancelAttack");
                myAnim.SetBool("isAttacking2", false);
                StartCoroutine(ReturnToWeaponIdle(1.5f,"isAttacking3"));
                thirdAttack = true;
                firstAttack = false;
                secondAttack = false;
                thirdAttack = false;
            }
            aggroUnit.TakeDamage(baseStats.attack);
            atkCooldown = baseStats.atkSpeed;
        }
    }

    private IEnumerator ReturnToWeaponIdle(float waitTime, string function)
    {
        yield return new WaitForSeconds(waitTime);
        //myAnim.SetBool("cancelAttack", true);
        myAnim.SetBool(function, false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Citizen"))
        {
            Farmear();
            Invoke("Salir", 1);
        }
    }

}
