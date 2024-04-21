using UnityEngine;

[CreateAssetMenu(fileName = "New Pickup", menuName = "Inventory/Health")]
public class HealthPickupData : ScriptableObject
{
    public string healthPickup;
    // For health spawn pools
    public GameObject healthPrefab;
    public int poolSize = 1;
    public int pickupRespawnTime;
    public int healAmount;
}