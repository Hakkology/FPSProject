using System.Collections;
using UnityEngine;

public abstract class PickupController : MonoBehaviour
{
    public LayerMask obstructionLayer;
    public float spawnCheckRadius = 1.0f;
    public int maxSpawnAttempts = 10;
    public float planeSizeX = 200f;
    public float planeSizeZ = 200f;

    protected abstract void TrySpawn();
    protected abstract IEnumerator Respawn(GameObject item);

    protected Vector3 GenerateRandomPosition()
    {
        float x = Random.Range(-planeSizeX / 2, planeSizeX / 2);
        float z = Random.Range(-planeSizeZ / 2, planeSizeZ / 2);
        return new Vector3(x, 0, z);
    }

    protected bool IsObstructed(Vector3 position)
    {
        Collider[] hitColliders = Physics.OverlapSphere(position, spawnCheckRadius, obstructionLayer);
        return hitColliders.Length > 0;
    }
}
