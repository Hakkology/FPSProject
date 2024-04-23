using System;
using TMPro;
using UnityEngine;

public class AmmoItemBehaviour : MonoBehaviour
{
    public AmmoType ammoType;
    public AmmoItemController ammoItemController;
    public TextMeshProUGUI ammoText;

    private int currentAmmo;

    void Start(){
        currentAmmo = ammoType.pickupSize;
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        ammoText.text = currentAmmo.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TryHandleAmmoPickup(other);
        }
    }

    private void TryHandleAmmoPickup(Collider other)
    {
        GunController gunController = other.gameObject.GetComponentInChildren<GunController>();
        if (gunController == null) return;
        GunBehaviour activeGunBehaviour = gunController.GetActiveGun();

        if (activeGunBehaviour != null && activeGunBehaviour.gunData.ammoType == ammoType)
        {
            int ammoToAdd = (int)activeGunBehaviour.gunData.ammoType.maxAmmoCapacity.CurrentValue - activeGunBehaviour.CurrentTotalAmmo;

            if (ammoToAdd >= ammoType.pickupSize)
            {
                int ammoToCollect = ammoType.pickupSize;
                activeGunBehaviour.AddAmmo(ammoToCollect);
                currentAmmo -= ammoToCollect;
                UpdateDisplay();
                gunController.RefreshAmmoDisplayForAllGuns();   
                ProcessPickupReturn();     
            }
            else
            {
                int ammoCollected = (int)activeGunBehaviour.gunData.ammoType.maxAmmoCapacity.CurrentValue - activeGunBehaviour.CurrentTotalAmmo;
                activeGunBehaviour.AddAmmo(ammoCollected);
                currentAmmo -= ammoCollected;
                gunController.RefreshAmmoDisplayForAllGuns();
            }
        }
        else
        {
            int index = gunController.gunData.FindIndex(gun => gun.ammoType == ammoType);
            if (index != -1)
            {
                var ammoData = gunController.ammoStore[index];
                int ammoToAdd = (int)ammoType.maxAmmoCapacity.CurrentValue - ammoData.totalAmmo;

                if (ammoToAdd > ammoType.pickupSize)
                {
                    ammoToAdd = ammoType.pickupSize;
                    ammoData.totalAmmo += ammoToAdd;
                    currentAmmo -= ammoToAdd;
                    gunController.ammoStore[index] = ammoData;
                    Debug.Log($"Updated ammo for {gunController.gunData[index].gunName}: {ammoData.totalAmmo}");
                    gunController.RefreshAmmoDisplayForAllGuns();
                    ProcessPickupReturn();
                }
                else
                {
                    ammoData.totalAmmo += ammoToAdd;
                    currentAmmo -= ammoToAdd;
                    gunController.ammoStore[index] = ammoData;
                    Debug.Log($"Updated ammo for {gunController.gunData[index].gunName}: {ammoData.totalAmmo}");
                    gunController.RefreshAmmoDisplayForAllGuns();
                }
            }
        }
    }

    private void ProcessPickupReturn()
    {
        ammoItemController.EnqueueItem(gameObject);
        ammoItemController.DecreaseActivePickups();
        gameObject.SetActive(false); 
    }
}

/*


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
                        if (activeGunBehaviour.gunData.ammoType.maxAmmoCapacity.CurrentValue - activeGunBehaviour.CurrentTotalAmmo > ammoType.pickupSize)
                        {
                            int ammoCollected = activeGunBehaviour.AddAmmo(ammoType.pickupSize);
                            gunController.UpdateAmmoDisplay();
                            gunController.RefreshAmmoDisplayForAllGuns();

                            if (ammoItemController != null)
                            {
                                ammoItemController.EnqueueItem(gameObject);  
                                ammoItemController.DecreaseActivePickups();  
                            }
                        }
                        else
                        {
                            int ammoCollected = (int)activeGunBehaviour.gunData.ammoType.maxAmmoCapacity.CurrentValue - activeGunBehaviour.CurrentTotalAmmo;
                            activeGunBehaviour.AddAmmo(ammoCollected);
                            currentAmmo -= ammoCollected;
                            gunController.UpdateAmmoDisplay();
                            gunController.RefreshAmmoDisplayForAllGuns();
                        }
                    }
                    else
                    {
                        int index = gunController.gunData.FindIndex(gun => gun.ammoType == ammoType);
                        if (index != -1)
                        {
                            var ammoData = gunController.ammoStore[index];
                            int ammoToAdd = Mathf.Min(ammoType.pickupSize, (int)ammoType.maxAmmoCapacity.CurrentValue - ammoData.totalAmmo);
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
*/
