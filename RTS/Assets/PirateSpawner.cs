using LP.FDG.Units.Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LP.FDG.InputManager;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
namespace LP.FDG.Units
{

    public class PirateSpawner : MonoBehaviour
    {
        public int Scene;
        public GameObject enemyUnitPrefab;
        public float spawnInterval = 5.0f;
        public Transform[] spawnPoints;
        public int maxEnemiesToSpawn = 10; // Set the maximum number of enemies to spawn

        public int spawnedEnemies = 0;
        private int currentSpawnPointIndex = 0; // Index to keep track of the current spawn point
        private Coroutine spawnCoroutine;
        public Transform enemyUnitParent;
        public Transform enemyWarriorParent;

        public AttackPointManager attackPointManager; // Reference to the AttackPointManager

        public Image winText;

        private void Start()
        {
            spawnCoroutine = StartCoroutine(SpawnEnemyUnits());
            attackPointManager = FindAnyObjectByType<AttackPointManager>();
        }


        private void Update()
        {
            if(spawnedEnemies == maxEnemiesToSpawn)
            {
                if (attackPointManager.AreAllEnemiesDefeated())
                {
                    //SceneManager.LoadScene(Scene);
                    winText.gameObject.SetActive(true);
                    StartCoroutine(NextDay());
                }

            }
        }


        private IEnumerator SpawnEnemyUnits()
        {
            while (spawnedEnemies < maxEnemiesToSpawn)
            {
                yield return new WaitForSeconds(spawnInterval);

                // Get the current spawn point
                Transform spawnPoint = spawnPoints[currentSpawnPointIndex];

                // Spawn an enemy unit at the selected spawn point
                Instantiate(enemyUnitPrefab, spawnPoint.position, spawnPoint.rotation, enemyWarriorParent);
                Units.UnitHandler.instance.SetBasicUnitStats(enemyUnitParent);

                spawnedEnemies++;

                attackPointManager.IncrementEnemyCount();
                // Switch to the other spawn point
                currentSpawnPointIndex = (currentSpawnPointIndex + 1) % spawnPoints.Length;
            }

            // Stop the coroutine after reaching the maximum number of enemies
            StopCoroutine(spawnCoroutine);
        }

        public void EnemyDestroyed()
        {
            spawnedEnemies--;

            // Decrement the enemy count in the AttackPointManager
            attackPointManager.DecrementEnemyCount();

            // Check if there are no enemies left
            if (spawnedEnemies == 0)
            {
                // Handle the event when there are no enemies left
                Debug.Log("No enemies left.");
            }
        }
        private IEnumerator NextDay()
        {
            yield return new WaitForSeconds(2.0f);
            SceneManager.LoadScene(Scene);
        }
    }
}

