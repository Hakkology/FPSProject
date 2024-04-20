using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoController : PickupController
{
    [SerializeField] private AmmoType ammoType;
    [SerializeField] private Transform pickupSpawnTransform;
    private Queue<GameObject> ammoQueue = new Queue<GameObject>();
    private float nextSpawnTime;

    void Start()
    {
        InitializeAmmoPool();
        nextSpawnTime = Time.time + ammoType.pickupRespawnTime;
        TrySpawn();
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            TrySpawn();
            nextSpawnTime = Time.time + ammoType.pickupRespawnTime;
        }
    }

    private void InitializeAmmoPool()
    {
        for (int i = 0; i < ammoType.poolSize; i++)
        {
            Debug.Log($"{ammoType.ammoName} pool initialized");
            GameObject ammo = Instantiate(ammoType.ammoPrefab, pickupSpawnTransform);
            ammo.SetActive(false);
            ammoQueue.Enqueue(ammo);
        }
    }

    protected override void TrySpawn()
    {
        if (ammoQueue.Count > 0)
        {
            for (int i = 0; i < maxSpawnAttempts; i++)
            {
                Vector3 potentialPosition = GenerateRandomPosition();
                if (!IsObstructed(potentialPosition))
                {
                    GameObject ammo = ammoQueue.Dequeue();
                    ammo.transform.position = potentialPosition;
                    ammo.SetActive(true);
                    StartCoroutine(Respawn(ammo));
                    break;
                }
            }
        }
    }

    protected override IEnumerator Respawn(GameObject ammo)
    {
        yield return new WaitForSeconds(ammoType.pickupRespawnTime);
        ammo.SetActive(false);
        ammoQueue.Enqueue(ammo);
    }

}
