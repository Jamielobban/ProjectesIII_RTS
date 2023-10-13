using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LP.FDG.Buildings
{
    public class BasicBuilding : MonoBehaviour
    {
        public BuildingStatTypes.Base baseStats;

        // In your Tower/Building script
        public Transform attackPoint1;
        public Transform attackPoint2;


        public float maxHealth, currentHealth;

        [SerializeField] private Image healthBarAmount, healtBarBackground;

        private void Start()
        {
            maxHealth = baseStats.health;
            //attackPoint1 = gameObject.transform.GetChild(0);
            //attackPoint2 = gameObject.transform.GetChild(1);
            // Register attack points with the AttackPointManager
            AttackPointManager.instance.availableAttackPoints.Add(attackPoint1);
            AttackPointManager.instance.availableAttackPoints.Add(attackPoint2);

            //healthBarAmount.enabled = false;
            //healtBarBackground.enabled = false;
        }

        private void Update()
        {
            if(currentHealth <maxHealth)
            {

            }
        }


        void HandleHealthBuilding()
        {

            Camera camera = Camera.main;
            gameObject.transform.LookAt(gameObject.transform.position + camera.transform.rotation* Vector3.forward,
                camera.transform.rotation* Vector3.up);

            healthBarAmount.fillAmount = currentHealth / maxHealth;
        }
    }

}
