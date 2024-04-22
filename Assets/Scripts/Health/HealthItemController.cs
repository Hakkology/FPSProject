using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthItemController : PickupController
{
    [SerializeField] private HealthPickupData healthData;
    [SerializeField] private Transform pickupSpawnTransform;
    private Queue<GameObject> hpQueue = new Queue<GameObject>();
    private int activePickups = 0;

    void Start()
    {
        InitializeHealthPool();
    }

    void Update(){
        if (activePickups < healthData.poolSize)
        {
            TrySpawn();
        }
    }

    private void InitializeHealthPool()
    {
        for (int i = 0; i < healthData.poolSize; i++)
        {
            GameObject hp = Instantiate(healthData.healthPrefab, pickupSpawnTransform);
            hp.SetActive(false);
            hpQueue.Enqueue(hp);
        }
    }

    protected override void TrySpawn()
    {
        if (hpQueue.Count > 0 && activePickups < healthData.poolSize)
        {
            Vector3 potentialPosition = GenerateRandomPosition();
            if (!IsObstructed(potentialPosition))
            {
                GameObject hp = hpQueue.Dequeue();
                hp.transform.position = potentialPosition;
                hp.SetActive(true);
                activePickups++;
                HealthItemBehaviour healthBehaviour = hp.GetComponent<HealthItemBehaviour>();
                if (healthBehaviour != null)
                {
                    healthBehaviour.healthController = this;
                }
            }
        }
    }

    public void EnqueueItem(GameObject item)
    {
        item.SetActive(false);
        hpQueue.Enqueue(item);
        DecreaseActivePickups();
    }

    public void DecreaseActivePickups()
    {
        if (activePickups > 0)
            activePickups--;
    }
}
