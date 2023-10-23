using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace LP.FDG.Buildings
{
    public class BasicBuilding : MonoBehaviour
    {
        public BuildingStatTypes.Base baseStats;

        // In your Tower/Building script
        public Transform attackPoint1;
        public Transform attackPoint2;
        public Transform attackPoint3;
        public Transform attackPoint4;
        public Transform attackPoint5;

        public bool isCastle;

        public float maxHealth, currentHealth;

        [SerializeField] private Image healthBarAmount, healtBarBackground;
        [SerializeField] Image youLose;
        bool fullHealth;
        private void Start()
        {
            fullHealth = true;
            if(!isCastle)
            {
                baseStats.health = 200;
            }
            else
            {
                baseStats.health = 1000;
            }
            //currentHealth = baseStats.health;
            maxHealth = baseStats.health;
            //attackPoint1 = gameObject.transform.GetChild(0);
            //attackPoint2 = gameObject.transform.GetChild(1);
            // Register attack points with the AttackPointManager
            AttackPointManager.instance.availableAttackPoints.Add(attackPoint1);
            AttackPointManager.instance.availableAttackPoints.Add(attackPoint2);
            if(isCastle)
            {
                AttackPointManager.instance.availableAttackPoints.Add(attackPoint3);
                AttackPointManager.instance.availableAttackPoints.Add(attackPoint4);
                AttackPointManager.instance.availableAttackPoints.Add(attackPoint5);
            }
            healthBarAmount.enabled = false;
            healtBarBackground.enabled = false;
        }

        private void Update()
        {
            if (isCastle)
            {
                if(baseStats.health <= 0)
                {
                    youLose.gameObject.SetActive(true);
                    StartCoroutine(waitForDelay());
                    //Time.timeScale = 0.0f;

                }
            }
            if ((baseStats.health < maxHealth) && fullHealth)
            {
                //Debug.Log("Dealth");
                fullHealth = false;
                healthBarAmount.enabled = true;
                healtBarBackground.enabled = true;
            }
            if (!fullHealth)
            {
                HandleHealthBuilding();
            }
            //Debug.Log(currentHealth);
        }


        void HandleHealthBuilding()
        {

            //Camera camera = Camera.main;
            //gameObject.transform.LookAt(gameObject.transform.position + camera.transform.rotation* Vector3.forward,
            //    camera.transform.rotation* Vector3.up);

            if(baseStats.health <= 0)
            {
                AttackPointManager.instance.ReleaseAttackPoint(attackPoint1);
                AttackPointManager.instance.ReleaseAttackPoint(attackPoint2);
                //Debug.Log("I have died");
                //Destroy(this.gameObject);
            }
            healthBarAmount.fillAmount = baseStats.health / maxHealth;
        }
        private  IEnumerator waitForDelay()
        {
            yield return new WaitForSeconds(2.0f);
            SceneManager.LoadScene(0);
        }
    }



}
