using UnityEngine;
using TMPro;

public class AmmoDisplay : MonoBehaviour
{
    [SerializeField] private GunController gunController;
    [SerializeField] private TextMeshProUGUI ammoText;
    private GunBehaviour currentGunBehaviour;

    void Awake()
    {
        if (gunController == null)
        {
            Debug.LogError("GunController component not found.");
            return;
        }

        gunController.onActiveGunChanged += HandleActiveGunChanged;
    }

    private void HandleActiveGunChanged(GunBehaviour newActiveGun)
    {
        if (currentGunBehaviour != null)
        {
            currentGunBehaviour.onAmmoChanged -= UpdateAmmoDisplay;
        }

        currentGunBehaviour = newActiveGun;
        if (currentGunBehaviour != null)
        {
            currentGunBehaviour.onAmmoChanged += UpdateAmmoDisplay;
            UpdateAmmoDisplay(currentGunBehaviour.CurrentAmmo, currentGunBehaviour.CurrentTotalAmmo); 
        }
    }

    private void UpdateAmmoDisplay(int currentAmmo, int currentTotalAmmo)
    {
        ammoText.text = $"{currentAmmo} / {currentTotalAmmo}";
    }

    void OnDisable()
    {
        if (currentGunBehaviour != null)
        {
            currentGunBehaviour.onAmmoChanged -= UpdateAmmoDisplay;
        }
    }
}
