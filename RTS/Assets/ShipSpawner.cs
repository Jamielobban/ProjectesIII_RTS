using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSpawner : MonoBehaviour

{

    public float minSpawnTime = 30.0f;
    public float maxSpawnTime = 45.0f;

    public GameObject enemyPrefab;
    public Transform spawnPosition;


    Quaternion spawnRotation = Quaternion.Euler(0, 90, 0);
    //Quaternion spawnQuaternion = Quaternion.Euler(;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            float randomSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
            Debug.Log(randomSpawnTime);
            yield return new WaitForSeconds(randomSpawnTime);

            //Vector2 spawnPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
            Instantiate(enemyPrefab, spawnPosition.transform.position, spawnRotation);
        }
    }
}
