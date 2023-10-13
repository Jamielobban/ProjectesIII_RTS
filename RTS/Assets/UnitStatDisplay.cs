using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace LP.FDG.Units
{
    public class UnitStatDisplay : MonoBehaviour
    {

        public float maxHealth, armor, currentHealth;

        [SerializeField] private Image healthBarAmount;

        private bool isPlayerUnit;
            bool resume = false;
            float timer = 0;
        // Start is called before the first frame update
        void Awake()
        {
            try
            {
                maxHealth = gameObject.GetComponentInParent<Player.PlayerUnit>().baseStats.health;

                armor = gameObject.GetComponentInParent<Player.PlayerUnit>().baseStats.armor;

                isPlayerUnit = true;

            }
            catch (System.Exception)
            {
                Debug.Log("Unit is Enemy");
                try
                {
                    maxHealth = gameObject.GetComponentInParent<Enemy.EnemyUnit>().baseStats.health;

                    armor = gameObject.GetComponentInParent<Enemy.EnemyUnit>().baseStats.armor;

                    isPlayerUnit = false;
                }
                catch (System.Exception)
                {

                    Debug.Log("No unit scripts found");
                }
  
            }
            currentHealth = maxHealth;
        }



        // Update is called once per frame
        void Update()
        {
            DelayStart();
            if (resume)
            {

                HandleHealth();
            }
        }

        public void TakeDamage(float damage)
        {
            float totalDamage = damage - armor;
            currentHealth -= totalDamage;
        }

        public void HandleHealth()
        {
            Camera camera = Camera.main;
            gameObject.transform.LookAt(gameObject.transform.position + camera.transform.rotation * Vector3.forward,
                camera.transform.rotation * Vector3.up);

            healthBarAmount.fillAmount = currentHealth / maxHealth;

            if (currentHealth <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
            if (isPlayerUnit)
            {
                InputManager.InputHandler.instance.selectedUnits.Remove(gameObject.transform.parent.gameObject.transform);
            }
            Debug.Log("Dead");
            Destroy(gameObject.transform.parent.gameObject);
        }

        void DelayStart()
        {
            timer = +Time.deltaTime;
            if(timer > 0.2f)
            {
                resume = true;
            }
        }
    }
}

