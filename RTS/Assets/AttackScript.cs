using LP.FDG.Units;
using LP.FDG.Units.Enemy;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    public SphereCollider axeCollider; // Reference to the collider attached to the axe
    public float attackCooldown = 1.0f; // Time between attacks
    private bool isAttacking = false;
    private PplayerMovement player;
    public GameObject enemyUnit;

    public List<GameObject> enemyList = new List<GameObject>();
    private void Start()
    {
        player = FindObjectOfType<PplayerMovement>();
        axeCollider = GetComponent<SphereCollider>();
    }
    private void Update()
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Enemy");
        enemyList.Clear();
        enemyList.AddRange(objectsWithTag);
    }
    public void StartAttack()
    {
        if (player.isAttacking)
        {
           // StartCoroutine(PerformAttack());
            foreach (GameObject obj in enemyList)
            {
                if(Vector3.Distance(obj.transform.position,this.transform.position) < 1.0f)
                {
                    //obj.GetComponentInChildren<UnitStatDisplay>().TakeDamage(player.baseStats.attack);
                    //StartCoroutine(WaitTilDamage(0.5f,obj));
                    if (!obj.GetComponent<EnemyUnit>().isDead)
                    {
                        obj.GetComponentInChildren<UnitStatDisplay>().TakeDamage(player.baseStats.attack);
                    }
                }
                //Debug.Log(Vector3.Distance(obj.transform.position, this.transform.position));
            }
        }
    }

    private IEnumerator PerformAttack()
    {
        //isAttacking = true;

        // Play your attack animation here
        // Example: animation.Play("SwingAxeAnimation");

        // Enable the axe collider to hit objects
        axeCollider.enabled = true;

        //Debug.Log("Doing this");
        // Wait for the animation to finish
        yield return new WaitForSeconds(1f);
        // Disable the axe collider
        axeCollider.enabled = false;

        //isAttacking = false;
    }

    private IEnumerator WaitTilDamage(float waitTime, GameObject obj)
    {
        yield return new WaitForSeconds(waitTime);
    }
}
