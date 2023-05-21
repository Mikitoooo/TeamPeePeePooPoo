using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;

    public GameObject[] enemyPrefabs;
    public Transform[] spawnPoints;

    public float initialSpawnDelay = 2f;
    public float spawnRate = 2f;
    public float spawnRateIncrease = 0.1f;
    public float startMaxEnemies;
    public float currentMaxEnemies;
    public float enemyIncreaseRate;

    int currentEnemyCount;
    float currentSpawnDelay;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        //Ensure there's only once instance of the enemy spawner script
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        //Initializing
        currentMaxEnemies = startMaxEnemies;
        currentEnemyCount = 0;
        currentSpawnDelay = initialSpawnDelay;
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= currentSpawnDelay && currentEnemyCount < currentMaxEnemies)
        {
            SpawnEnemy();
            timer = 0f;
            currentSpawnDelay -= spawnRateIncrease;
            currentMaxEnemies = currentMaxEnemies + enemyIncreaseRate;
        }
    }

    private void SpawnEnemy()
    {
        int rng = Random.Range(0, 100);

        if (rng >= 0 && rng <= 45)
        {
            // case 0 - 45
            // Spawn small chase enemy
            Instantiate(enemyPrefabs[0], spawnPoints[Random.Range(0, spawnPoints.Length - 1)].position, transform.rotation);
            currentEnemyCount = currentEnemyCount + 1;
        }
        else if (rng > 45 && rng <= 90)
        {
            // case 45 - 90
            // Spawn small flying enemy
            Instantiate(enemyPrefabs[1], spawnPoints[Random.Range(1, spawnPoints.Length - 1)].position, transform.rotation);
            currentEnemyCount = currentEnemyCount + 1;
        }
        else if (rng > 90 && rng <= 95)
        {
            // case 90 - 95
            // Spawn large chase enemy
            Instantiate(enemyPrefabs[2], spawnPoints[Random.Range(2, spawnPoints.Length - 1)].position, transform.rotation);
            currentEnemyCount = currentEnemyCount + 3;
        }
        else if (rng > 95 && rng <= 100)
        {
            // case 95 - 100
            // Spawn large fly enemy
            Instantiate(enemyPrefabs[3], spawnPoints[Random.Range(3, spawnPoints.Length - 1)].position, transform.rotation);
            currentEnemyCount = currentEnemyCount + 4;
        }
        

        // Instantiate the enemy prefab at the spawner's position and rotation
        //Instantiate(enemyPrefabs[0], spawnPoints[Random.Range(0, spawnPoints.Length - 1)].position, transform.rotation);
        //currentEnemyCount++;
    }

    public void EnemyDestroyed(int value)
    {
        currentEnemyCount = currentEnemyCount - value;
    }
}
