using UnityEngine;
using TMPro;

public class AmmoItemBehaviour : MonoBehaviour
{
    public AmmoType ammoType;
    public TMP_Text ammoDisplay;

    private void Start()
    {
        InitializeAmmo();
    }

    private void OnEnable()
    {
        InitializeAmmo();
    }

    private void InitializeAmmo()
    {
        UpdateAmmoDisplay();
    }

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
                    UpdateAmmoDisplay();
                }
                else
                {
                    gameObject.SetActive(false);
                    ammoType.pickupSize = playerGunBehaviour.gunData.ammoType.pickupSize;
                }
            }
        }
    }

    private void UpdateAmmoDisplay()
    {
        if (ammoDisplay != null)
        {
            ammoDisplay.text = ammoType.pickupSize.ToString();
        }
    }
}
