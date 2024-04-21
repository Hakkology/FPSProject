using UnityEngine;

public class AmmoItemBehaviour : MonoBehaviour
{
    public AmmoType ammoType;
    public AmmoItemController ammoItemController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GunController gunController = other.GetComponentInChildren<GunController>();
            if (gunController != null)
            {
                GunBehaviour activeGunBehaviour = gunController.GetActiveGun();
                if (activeGunBehaviour != null)
                {
                    if (activeGunBehaviour.gunData.ammoType == ammoType)
                    {
                        int ammoCollected = activeGunBehaviour.AddAmmo(ammoType.pickupSize);
                        gunController.UpdateAmmoDisplay();
                    }
                    else
                    {
                        int index = gunController.gunData.FindIndex(gun => gun.ammoType == ammoType);
                        if (index != -1)
                        {
                            var ammoData = gunController.ammoStore[index];
                            int ammoToAdd = Mathf.Min(ammoType.pickupSize, ammoType.maxAmmoCapacity - ammoData.totalAmmo);
                            ammoData.totalAmmo += ammoToAdd;
                            gunController.ammoStore[index] = ammoData;
                            Debug.Log($"Updated ammo for {gunController.gunData[index].gunName}: {ammoData.totalAmmo}");
                        }
                    }
                }
                gunController.RefreshAmmoDisplayForAllGuns();

                if (ammoItemController != null)
                {
                    ammoItemController.EnqueueItem(gameObject);  
                    ammoItemController.DecreaseActivePickups();  
                }
            }
        }
    }
}
