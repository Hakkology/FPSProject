using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPoolManager : MonoBehaviour
{
    public List<AmmoType> ammoTypes;

    private Dictionary<AmmoType, Queue<GameObject>> ammoDict = new Dictionary<AmmoType, Queue<GameObject>>();

    private void Start()
    {
        // Initialize each pool based on ammoTypes
        foreach (var ammoType in ammoTypes)
        {
            Queue<GameObject> queue = new Queue<GameObject>();
            for (int i = 0; i < ammoType.poolSize; i++)
            {
                GameObject ammo = Instantiate(ammoType.ammoPrefab);
                ammo.SetActive(false);
                queue.Enqueue(ammo);
            }
            ammoDict[ammoType] = queue;
        }
    }

    public void SpawnAmmo(Vector3 position, AmmoType type)
    {
        if (ammoDict.ContainsKey(type) && ammoDict[type].Count > 0)
        {
            GameObject ammo = ammoDict[type].Dequeue();
            ammo.transform.position = position;
            ammo.SetActive(true);
            StartCoroutine(RespawnAmmo(ammo, type));
        }
    }

    private IEnumerator RespawnAmmo(GameObject ammo, AmmoType type)
    {
        yield return new WaitForSeconds(type.pickupRespawnTime);
        ammo.SetActive(false);
        if (ammoDict.ContainsKey(type))
        {
            ammoDict[type].Enqueue(ammo);
        }
    }
}
