using System.Collections;
using System.Collections.Generic;
//using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
//using System;
using LP.FDG.Units;
using UnityEngine.UI;
using UnityEngine.Rendering;
//using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;
using Unity.VisualScripting.FullSerializer;
using System.Net.Mime;
using UnityEngine.AI;
using DG.Tweening;
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
    public bool isInCombat;

    [SerializeField]
    private RuntimeAnimatorController combatController;
    [SerializeField]
    private RuntimeAnimatorController playingController;
    [SerializeField]
    private GameObject axeToEnable;

    [SerializeField]
    private BoxCollider attackCollider;

    public bool firstAttack;
    public bool secondAttack;
    public bool thirdAttack;
    public bool returnToIdle;
    public bool isAttacking;

    int RandomNumber;

    public AttackScript attackScript;

    public AudioSource audioSource;
    public AudioClip running;
    public AudioClip swordSwing;
    public AudioClip interact;
    bool doOnce;
    // Start is called before the first frame update
    void Start()
    {
        doOnce = true;
        firstAttack = false;
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
        {

            return;
        }
        HandleMovement();

        if (isInCombat)
        {
            myAnim.runtimeAnimatorController = combatController;
            axeToEnable.SetActive(true);
        }
        else
        {
            myAnim.runtimeAnimatorController = playingController;
            axeToEnable.SetActive(false);
        }

        if (isRunning)
        {
            if (audioSource.clip != running || !audioSource.isPlaying)
            {
                audioSource.volume = 1.0f;
                audioSource.clip = running;
                audioSource.loop = true;
                audioSource.Play();
            }

            myAnim.SetBool("isRunning", true);
        }
        else
        {
            if (audioSource.clip == running && audioSource.isPlaying)
            {
                audioSource.Stop();
            }

            myAnim.SetBool("isRunning", false);
        }
    }

    private void LateUpdate()
    {
        if (acabado)
        {
            return;

        }

       

        if (isInCombat && canMove && Input.GetMouseButtonDown(0))
        {
            if (isRunning)
            {
                Debug.Log("Wait a second noo");
                StartCoroutine(WaitDelay(0.3f));
            }
            else
            {
                canMove = false; // Prevent movement during the attack
                // Trigger the attack
                //attackScript.StartAttack();
                //var myTween = myAnim.SetBool("isAttacking1", true);
                isAttacking = true;

                RandomNumber = Random.Range(0, 3);
            }

            //DOVirtual.DelayedCall(delay, () => {
            //    myAnim.SetBool("isAttacking2", true); // Set the animation bool to false when the animation is done
            //});
        }

        if(isAttacking)
        {
            attackScript.StartAttack();
            //audioSource.clip = swordSwing;
            audioSource.clip = swordSwing;
            audioSource.loop = false;
            audioSource.volume = 0.1f;
            audioSource.Play();
            switch (RandomNumber)
            {
                case 0:
                    myAnim.SetBool("isAttacking1", true);
                    isAttacking = false;
                    StartCoroutine(CancelAnim("isAttacking1"));
                    break;
                case 1:
                    myAnim.SetBool("isAttacking2", true);
                    isAttacking = false;
                    StartCoroutine(CancelAnim("isAttacking2"));
                    break;
                case 2:
                    myAnim.SetBool("isAttacking3", true);
                    isAttacking = false;
                    StartCoroutine(CancelAnim("isAttacking3"));
                    break;
                default:
                    break;
            }
        }
        //if (isAttacking)
        //{
        //    //flagCheck = true;
        //    if (!firstAttack)
        //    {
        //        firstAttack = true;
        //        myAnim.SetBool("isAttacking1", true);
        //    }
        //    animationLength = myAnim.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        //    delay = animationLength + 0.2f;
        //    chainAttackTimer1 += Time.deltaTime;
        //    if ((chainAttackTimer1 > delay / 2) && (chainAttackTimer1 < delay))
        //    {
        //        //Debug.Log("When");
        //        if (Input.GetMouseButtonDown(0) && !secondAttackClick)
        //        {
        //            secondAttackClick = true;
        //            Debug.Log("Somehow");
        //        }
        //    }
        //    else if (!secondAttackClick)
        //    {
        //        //DOVirtual.DelayedCall(delay, () =>
        //        //{
        //        //    myAnim.SetBool("backToIdle", true); // Set the animation bool to false when the animation is done
        //        //});
        //        //Debug.Log("Done attack i guess");
        //        Debug.Log("Doingt this");
        //        DOVirtual.DelayedCall(delay, () =>
        //        {
        //            myAnim.SetBool("isAttacking1", false); isAttacking = false; firstAttack = false; canMove = true;// Set the animation bool to false when the animation is done
        //        });
        //    }

        //    if (secondAttackClick)
        //    {
        //        //Debug.Log("Wow");
        //    }
        //}
        //CheckForEnemyTargets();
        //if (hasAggro)
        //{
        //    atkCooldown -= Time.deltaTime;
        //    Attack();
        //}

    }

    private IEnumerator CancelAnim(string type)
    {
        yield return new WaitForSeconds(myAnim.GetCurrentAnimatorClipInfo(0)[0].clip.length + 0.2f);
        myAnim.SetBool(type, false);
        canMove = true;
    }

    private IEnumerator WaitDelay(float waitTime)
    {
        canMove = false;
        yield return new WaitForSeconds(waitTime);
        isAttacking = true;
        RandomNumber = Random.Range(0, 3);
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
            //doOnce = true;
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

        //if(!isInCombat && Input.GetKey(KeyCode.R))
        //{
        //    axeToEnable.SetActive(true);
        //    isInCombat = true;
        //}
        //if(isInCombat && Input.GetKey(KeyCode.T))
        //{
        //    axeToEnable.SetActive(false);
        //    isInCombat = false;
        //}
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
        audioSource.clip = interact;
        audioSource.loop = false;
        audioSource.volume = 1.0f;
        audioSource.Play();
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

    //private void CheckForEnemyTargets()
    //{
    //    rangeColliders = Physics.OverlapSphere(transform.position, baseStats.aggroRange);
    //    //Debug.Log("Doing this");
    //    for (int i = 0; i < rangeColliders.Length; i++)
    //    {
    //        if (rangeColliders[i].gameObject.layer == UnitHandler.instance.eUnitLayer)
    //        {
    //            aggroTarget = rangeColliders[i].gameObject.transform;
    //            aggroUnit = aggroTarget.gameObject.GetComponentInChildren<UnitStatDisplay>();
    //            hasAggro = true;
    //            //Debug.Log("Enemy found");
    //             distance = Vector3.Distance(aggroTarget.position, transform.position);
    //            //Debug.Log(distance);
                
    //            //Debug.Log(("ENEMY LOCATED"));
    //            break;
    //        }
    //        else
    //        {
    //            //if(myAnim.runtimeAnimatorController == combatController)
    //            //{
    //            //    firstAttack = false;
    //            //    secondAttack = false;
    //            //    thirdAttack = false;
    //            //    myAnim.SetBool("cancelAttack", true);
    //            //}

    //            distance = 0.0f;
    //            hasAggro = false;
    //            aggroTarget = null; 
    //            aggroUnit = null;
    //            //myAnim.SetBool("cancelAttack", true);
    //            //StartCoroutine(ReturnToWeaponIdle(1.5f, "cancelAttack"));
    //        }
    //    }
    //}

    private void Attack()
    {
        //if (atkCooldown <= 0 /*&& distance <= baseStats.atkRange + 1*/ )
        ////{
        ////    //Debug.Log("In here");

        ////    if (myAnim.runtimeAnimatorController == combatController && firstAttack == false)
        ////    {
        ////        myAnim.SetBool("isAttacking1", true);
        ////        firstAttack = true;
        ////    }
        ////    else if (myAnim.runtimeAnimatorController == combatController && secondAttack == false && firstAttack == true)
        ////    {
        ////        myAnim.SetBool("isAttacking2", true);
        ////        myAnim.SetBool("isAttacking1", false);
        ////        secondAttack = true;
        ////    }
        ////    else if(myAnim.runtimeAnimatorController == combatController && thirdAttack == false && firstAttack == true && secondAttack == true)
        ////    {
        ////        myAnim.SetBool("isAttacking3", true);
        ////        //Debug.Log("CancelAttack");
        ////        myAnim.SetBool("isAttacking2", false);
        ////        StartCoroutine(ReturnToWeaponIdle(1.5f,"isAttacking3"));
        ////        thirdAttack = true;
        ////        firstAttack = false;
        ////        secondAttack = false;
        ////        thirdAttack = false;
        //    }
        //}
        aggroUnit.TakeDamage(baseStats.attack);
        atkCooldown = baseStats.atkSpeed;
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
