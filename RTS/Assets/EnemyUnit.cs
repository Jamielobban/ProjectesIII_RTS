using LP.FDG.Buildings;
using System.Collections;
using System.Collections.Generic;
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

        public bool hasTarget = false;
        public bool hasArrived = false;
        public bool isAttacking = false;
        private void Start()
        {
            navAgent = gameObject.GetComponent<NavMeshAgent>();

        }

       

        private void Update()
        {
            if(!hasTarget)
            {
                AttackBuilding(attackPoint);
            }
            else if(!hasArrived)
            {
                if(Vector3.Distance(this.transform.position,attackPoint.transform.position) < 3)
                {
                    hasArrived=true;
                    Debug.Log(attackPoint.transform.GetComponentInParent<BasicBuilding>().baseStats.health);
                    isAttacking = true;
                }
            }
            else if (isAttacking)
            {
                AttackBuilding(baseStats.attack);
            }
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
            atkCooldown -= Time.deltaTime;
            if(atkCooldown <= 0)
            {
                Debug.Log(attackPoint.transform.GetComponentInParent<BasicBuilding>().baseStats.health);
                attackPoint.transform.GetComponentInParent<BasicBuilding>().baseStats.health -= damage;
                atkCooldown = baseStats.atkSpeed;
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

        private void AttackBuilding(Transform buildingTransform)
        {
            attackPoint = AttackPointManager.instance.ReserveAttackPoint();

            if (attackPoint != null)
            {
                // Move to the attack point
                navAgent.SetDestination(attackPoint.position);
                hasTarget = true;
            }
            else
            {
                
            }
        }

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
    }
}

