using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickupController : PickupController
{
    [SerializeField] private HealthPickupData healthData;
    [SerializeField] private Transform pickupSpawnTransform;
    private Queue<GameObject> hpQueue = new Queue<GameObject>();
    private float nextSpawnTime;

    void Start()
    {
        InitializeHealthPool();
        nextSpawnTime = Time.time + healthData.pickupRespawnTime;
        TrySpawn();
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            TrySpawn();
            nextSpawnTime = Time.time + healthData.pickupRespawnTime;
        }
    }

    private void InitializeHealthPool()
    {
        for (int i = 0; i < healthData.poolSize; i++)
        {
            Debug.Log($"{healthData.healthPickup} pool initialized");
            GameObject hp = Instantiate(healthData.healthPrefab, pickupSpawnTransform);
            hp.SetActive(false);
            hpQueue.Enqueue(hp);
        }
    }

    protected override void TrySpawn()
    {
        if (hpQueue.Count > 0)
        {
            for (int i = 0; i < maxSpawnAttempts; i++)
            {
                Vector3 potentialPosition = GenerateRandomPosition();
                if (!IsObstructed(potentialPosition))
                {
                    GameObject hp = hpQueue.Dequeue();
                    hp.transform.position = potentialPosition;
                    hp.SetActive(true);
                    StartCoroutine(Respawn(hp));
                    break;
                }
            }
        }
    }

    protected override IEnumerator Respawn(GameObject hp)
    {
        yield return new WaitForSeconds(healthData.pickupRespawnTime);
        hp.SetActive(false);
        hpQueue.Enqueue(hp);
    }

}
