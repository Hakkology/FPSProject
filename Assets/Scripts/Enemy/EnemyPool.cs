using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPooler : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnLocations;
    public EnemyData enemyData;
    public Transform enemySpawnTransform;
    
    private Queue<GameObject> enemyPool = new Queue<GameObject>();
    private List<GameObject> activeEnemies = new List<GameObject>();

    void Start()
    {
        InitializePool();
    }

    void InitializePool()
    {
        for (int i = 0; i < enemyData.enemyPoolSize; i++)
        {
            GameObject obj = Instantiate(enemyPrefab, enemySpawnTransform);
            obj.SetActive(false); // Instantiate with inactive state to prevent premature OnEnable
            EnemyBehaviour enemyBehaviour = obj.GetComponent<EnemyBehaviour>();
            if (enemyBehaviour != null)
            {
                enemyBehaviour.enemyPool = this;
                Debug.Log("EnemyPooler assigned to EnemyBehaviour.");
            }
            else
            {
                Debug.LogError("Failed to find EnemyBehaviour component on the instantiated enemy prefab.");
            }
            enemyPool.Enqueue(obj);
        }
        FillActiveEnemies();
    }

    void FillActiveEnemies()
    {
        while (activeEnemies.Count < enemyData.enemyPoolSize && enemyPool.Count > 0)
        {
            SpawnEnemy();
        }
    }

    public void SpawnEnemy()
    {
        if (enemyPool.Count == 0)
        {
            Debug.LogWarning("No enemies available in pool.");
            return;
        }

        GameObject enemy = enemyPool.Dequeue();
        int spawnIndex = Random.Range(0, spawnLocations.Length);
        enemy.transform.position = spawnLocations[spawnIndex].position;
        
        var enemyAgent = enemy.GetComponent<NavMeshAgent>();
        if (enemyAgent != null)
        {
            enemyAgent.enabled = true;
            enemyAgent.ResetPath();
        }

        enemy.SetActive(true);
        activeEnemies.Add(enemy);
        Debug.Log($"Enemy spawned at {spawnLocations[spawnIndex].position}");
    }

    public void DespawnEnemy(GameObject enemy)
    {
        enemy.SetActive(false);
        enemyPool.Enqueue(enemy);
        activeEnemies.Remove(enemy);
        StartCoroutine(RespawnEnemy(enemy));
    }

    IEnumerator RespawnEnemy(GameObject enemy)
    {
        yield return new WaitForSeconds(enemyData.enemySpawnCooldown);
        SpawnEnemy();  
    }
}
