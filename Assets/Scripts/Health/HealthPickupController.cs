using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickupController : PickupController
{
    [SerializeField] private HealthPickupData healthData;
    [SerializeField] private Transform pickupSpawnTransform;
    private Queue<GameObject> hpQueue = new Queue<GameObject>();

    void Start()
    {
        InitializeHealthPool();
        TrySpawn();
    }

    private void InitializeHealthPool()
    {
        for (int i = 0; i < healthData.poolSize; i++)
        {
            GameObject hp = Instantiate(healthData.healthPrefab, pickupSpawnTransform);
            hp.SetActive(false);
            HealthItemBehaviour healthItemBehaviour = hp.GetComponent<HealthItemBehaviour>();
            if (healthItemBehaviour != null)
            {
                healthItemBehaviour.healthController = this; 
            }
            else
            {
                Debug.LogError("HealthItemBehaviour component not found on the health pickup prefab.");
            }
            hpQueue.Enqueue(hp);
        }
    }

    protected override void TrySpawn()
    {
        if (hpQueue.Count > 0)
        {
            Vector3 potentialPosition = GenerateRandomPosition();
            if (!IsObstructed(potentialPosition))
            {
                GameObject hp = hpQueue.Dequeue();
                hp.transform.position = potentialPosition;
                hp.SetActive(true);
            }
        }
    }

    public void StartRespawnCoroutine(GameObject hp)
    {
        StartCoroutine(Respawn(hp));
    }

    protected override IEnumerator Respawn(GameObject hp)
    {
        yield return new WaitForSeconds(healthData.pickupRespawnTime);
        hp.SetActive(false);
        hpQueue.Enqueue(hp);
        TrySpawn();
    }
}
