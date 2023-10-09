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


        public List<GameObject> taggedObjectsList = new List<GameObject>();
        public GameObject buildingToAttack;
        private void Start()
        {
            navAgent = gameObject.GetComponent<NavMeshAgent>();
            GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("BuildingAttackPosition");
            taggedObjectsList.AddRange(objectsWithTag);
            //foreach (GameObject obj in objectsWithTag)
            //{
            //    attackPosition.Add(obj);

            //}

        }

        GameObject FindClosestAttackPoint(Vector3 referencePoint)
        {
            GameObject closestObject = null;
            float closestDistance = Mathf.Infinity;

            foreach (GameObject obj in taggedObjectsList)
            {
                float distance2 = Vector3.Distance(referencePoint, obj.transform.position);

                if(distance2 < closestDistance)
                {
                    closestDistance = distance2;
                    closestObject = obj;
                }
            }
            return closestObject;
        }

        private void Update()
        {

            Vector3 characterPosition = transform.position;
            GameObject closestObject = FindClosestAttackPoint(characterPosition);

            if(closestObject != null)
            {
                navAgent.SetDestination(closestObject.transform.position);
            }
            //atkCooldown -= Time.deltaTime;


            //if(!hasAggro)
            //{
            //    CheckForEnemyTargets();
            //}
            //else
            //{
            //    Attack();
            //    MoveToAggroTarget();
            //}
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

      
    }
}

