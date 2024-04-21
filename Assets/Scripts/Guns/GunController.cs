using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{

    public delegate void ActiveGunChanged(GunBehaviour newActiveGun);
    public event ActiveGunChanged onActiveGunChanged;

    public List<Gun> gunData;
    public Transform gunHolder;
    private List<GameObject> weapons = new List<GameObject>();
    private int currentWeaponIndex = 0;

    void Start()
    {
        PrepareWeapons();
    }

    private void PrepareWeapons()
    {
        foreach (Gun gun in gunData)
        {
            Vector3 position = gunHolder.position + gunHolder.TransformDirection(gun.customPosition);
            GameObject weapon = Instantiate(gun.gun, position, Quaternion.identity, gunHolder);
            GunBehaviour gunBehaviour = weapon.AddComponent<GunBehaviour>();
            gunBehaviour.gunData = gun;

            weapon.SetActive(false);
            weapons.Add(weapon);
        }

        weapons[currentWeaponIndex].SetActive(true);
        UpdateAmmoDisplay();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwitchWeapon();
            UpdateAmmoDisplay();
        }
    }

    void SwitchWeapon()
    {
        weapons[currentWeaponIndex].SetActive(false);
        currentWeaponIndex = (currentWeaponIndex + 1) % weapons.Count;
        weapons[currentWeaponIndex].SetActive(true);

        // For hp testing
        PlayerHealth health = GetComponentInParent<PlayerHealth>();
        health.TakeDamage(10);
    }

    public void UpdateAmmoDisplay(){
        onActiveGunChanged?.Invoke(weapons[currentWeaponIndex].GetComponent<GunBehaviour>());
    }
}
