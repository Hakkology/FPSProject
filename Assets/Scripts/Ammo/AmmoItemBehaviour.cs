using UnityEngine;
using TMPro;

public class AmmoItemBehaviour : MonoBehaviour
{
    public AmmoType ammoType;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GunBehaviour playerGunBehaviour = other.GetComponentInChildren<GunBehaviour>();
            if (playerGunBehaviour != null && playerGunBehaviour.gunData.ammoType == ammoType)
            {
                int ammoCollected = playerGunBehaviour.AddAmmo(ammoType.pickupSize);
                
                if (ammoType.pickupSize > ammoCollected)
                {
                    ammoType.pickupSize -= ammoCollected;
                }
                else
                {
                    gameObject.SetActive(false);
                    ammoType.pickupSize = playerGunBehaviour.gunData.ammoType.pickupSize;
                }
            }
        }
    }
}
