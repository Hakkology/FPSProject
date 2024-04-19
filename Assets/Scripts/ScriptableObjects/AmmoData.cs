using UnityEngine;

[CreateAssetMenu(fileName = "New AmmoType", menuName = "Inventory/AmmoType")]
public class AmmoType : ScriptableObject
{
    public string ammoName;
    public int maxAmmoCapacity; 
    public int clipSize; 
    public float reloadTime; 
}