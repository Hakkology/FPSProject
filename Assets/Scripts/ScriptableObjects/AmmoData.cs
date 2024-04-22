using UnityEngine;

[CreateAssetMenu(fileName = "New AmmoType", menuName = "Inventory/AmmoType")]
public class AmmoType : ScriptableObject
{
    public string ammoName;
    // For ammo spawn pools
    public GameObject ammoPrefab;
    public int poolSize = 3;
    public int pickupRespawnTime;
    public int pickupSize;

    // For ammo behaviour
    public ScalableTalent maxAmmoCapacity; 
    public int startingMaxAmmo;
    public int clipSize; 
    public float reloadTime; 
    public float fireRate;
}