using DG.Tweening;
using LP.FDG.Buildings;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace LP.FDG.Units.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyUnit : MonoBehaviour
    {

        private NavMeshAgent navAgent;

        public UnitStatTypes.Base baseStats;

        private Collider[] rangeColliders;

        private Transform aggroTarget;

        private UnitStatDisplay aggroUnit;

        private bool hasAggro =  false;

        private float distance;

        private float atkCooldown;

        public Transform attackPoint;

        public Animator animController;

        public bool hasTarget = false;
        public bool hasArrived = false;
        public bool isAttacking = false;


        Vector3 targetPosition;
        Quaternion targetRotation;

        public bool isDead;


        public AudioSource audioSource;
        public AudioClip attackSound;
        public AudioClip hitSound;
        public AudioClip runSound;

        private void Start()
        {
            navAgent = gameObject.GetComponent<NavMeshAgent>();
            //animController = gameObject.GetComponent<Animator>();
        }

       

        private void Update()
        {
            if (!isDead)
            {
                if(!hasTarget)
                {
                    FindAndGoToClosestAttackPoint();
                    //WalkToBuilding(attackPoint);
                }
                else if(!hasArrived)
                {
                    if (!audioSource.isPlaying || audioSource.clip != runSound)
                    {
                        audioSource.clip = runSound;
                        audioSource.volume = 1.0f;
                        audioSource.loop = true;
                        audioSource.Play();
                    }

                    if (Vector3.Distance(this.transform.position,attackPoint.transform.position) < 1 && !hasArrived)
                    {
                        hasArrived=true;
                        navAgent.updateRotation = false;
                        targetPosition = attackPoint.transform.parent.transform.position;
                        targetRotation = Quaternion.LookRotation(targetPosition - transform.position);
                        targetRotation.x= transform.rotation.x;
                        //transform.LookAt(attackPoint.transform.parent);
                        //Debug.Log(attackPoint.transform.parent);

                        transform.DORotateQuaternion(targetRotation,1f).OnComplete(() => isAttacking = true);
                    }
                }
                else if (isAttacking)
                {
                    //if(attackPoint == null)
                    //{
                    //    isAttacking = false;
                    //    hasTarget = false;
                    //}
                    if (audioSource.clip == runSound && audioSource.isPlaying)
                    {
                        audioSource.Stop();
                    }
                    AttackBuilding(baseStats.attack);
                }
                
            }
            else
            {
                animController.SetBool("isDead", true);
                audioSource.clip = hitSound;
                audioSource.volume = 1.0f;
                audioSource.loop = false;
                audioSource.Play();
                this.gameObject.GetComponent<BoxCollider>().enabled = false;
                //audioSource.Stop();
            }
        }

        private void FindAndGoToClosestAttackPoint()
        {
            Transform closestAttackPoint = AttackPointManager.instance.ReserveClosestAttackPoint(transform.position);
            if (closestAttackPoint != null)
            {
                attackPoint = closestAttackPoint;

                // Move to the closest attack point
                navAgent.SetDestination(attackPoint.position);
                hasTarget = true;
            }
        }

        private Transform FindClosestAttackPoint()
        {
            Transform closestPoint = null;
            float closestDistance = float.MaxValue;

            foreach (Transform point in AttackPointManager.instance.availableAttackPoints)
            {
                float distance = Vector3.Distance(transform.position, point.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestPoint = point;
                }
            }
            //Debug.Log(closestPoint);
            return closestPoint;
        }

        private void CheckForEnemyTargets()
        {
            rangeColliders = Physics.OverlapSphere(transform.position, baseStats.aggroRange);

            for (int i = 0; i < rangeColliders.Length; i++)
            {
                if (rangeColliders[i].gameObject.layer == UnitHandler.instance.pUnitLayer)
                {
                    aggroTarget = rangeColliders[i].gameObject.transform;
                    aggroUnit = aggroTarget.gameObject.GetComponentInChildren<UnitStatDisplay>();
                    hasAggro = true;
                    break;
                }
            }
        }


        private void Attack()
        {
            if(atkCooldown <= 0 && distance <= baseStats.atkRange + 1)
            {

                aggroUnit.TakeDamage(baseStats.attack);
                atkCooldown = baseStats.atkSpeed;
            }
        }

        void AttackBuilding(float damage)
        {
            if (attackPoint.transform.GetComponentInParent<BasicBuilding>().baseStats.health <=0)
            {
                attackPoint = null;
                isAttacking = false;
                hasArrived = false;
                hasTarget = false;
                navAgent.updateRotation = true;
                animController.SetBool("isShooting", false);
                //Debug.Log("is it there");
                return;
            }
            atkCooldown -= Time.deltaTime;
            if(atkCooldown <= 0)
            {
                animController.SetBool("isShooting",true);
                //Debug.Log(attackPoint.transform.GetComponentInParent<BasicBuilding>().baseStats.health);
                attackPoint.transform.GetComponentInParent<BasicBuilding>().baseStats.health -= damage;
                atkCooldown = baseStats.atkSpeed;
                if (!audioSource.isPlaying)
                {
                    audioSource.clip = attackSound;  // Set the audio clip
                    audioSource.volume = 0.2f;
                    audioSource.loop = true;        // Enable looping
                    audioSource.Play();             // Play the audio
                }
            }
        }
    

        private void MoveToAggroTarget()
        {
            if(aggroTarget == null)
            {
                navAgent.SetDestination(transform.position);
                hasAggro = false;
            }
            else
            {

                distance = Vector3.Distance(aggroTarget.position, transform.position);
                navAgent.stoppingDistance = (baseStats.atkRange + 1);


                if (distance <= baseStats.aggroRange)
                {
                    navAgent.SetDestination(aggroTarget.position);
                }
            }
        }

        //private void WalkToBuilding(Transform buildingTransform)
        //{
        //    attackPoint = AttackPointManager.instance.ReserveAttackPoint();

        //    if (attackPoint != null)
        //    {
        //        // Move to the attack point
        //        navAgent.SetDestination(attackPoint.position);
        //        hasTarget = true;
        //    }
        //    else
        //    {
                
        //    }
        //}

        // When the attack is done
        private void FinishAttack()
        {
            if (attackPoint != null)
            {
                // Release the attack point
                AttackPointManager.instance.ReleaseAttackPoint(attackPoint);
                attackPoint = null;
            }
        }

        public void TakeDamage()
        {

        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Axe"))
            {
                Debug.Log("hit");
            }
        }
    }
}

