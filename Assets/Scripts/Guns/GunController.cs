using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GunController : MonoBehaviour
{

    public delegate void ActiveGunChanged(GunBehaviour newActiveGun);
    public event ActiveGunChanged onActiveGunChanged;
    public Dictionary<int, (int currentAmmo, int totalAmmo)> ammoStore = new Dictionary<int, (int, int)>();

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
            ammoStore.Add(weapons.Count - 1, (gun.ammoType.clipSize, gun.ammoType.startingMaxAmmo));

            var ammoData = ammoStore[weapons.Count - 1];
            gunBehaviour.SetAmmo(ammoData.currentAmmo, ammoData.totalAmmo);
            
        }

        if (weapons.Any())
        {
            weapons[currentWeaponIndex].SetActive(true);
            UpdateAmmoDisplay();
        }
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
        GunBehaviour currentGun = weapons[currentWeaponIndex].GetComponent<GunBehaviour>();
        ammoStore[currentWeaponIndex] = (currentGun.CurrentAmmo, currentGun.CurrentTotalAmmo);

        weapons[currentWeaponIndex].SetActive(false);
        currentWeaponIndex = (currentWeaponIndex + 1) % weapons.Count;
        weapons[currentWeaponIndex].SetActive(true);

        var ammoData = ammoStore[currentWeaponIndex];
        GunBehaviour newActiveGun = weapons[currentWeaponIndex].GetComponent<GunBehaviour>();
        newActiveGun.SetAmmo(ammoData.currentAmmo, ammoData.totalAmmo);

        if (onActiveGunChanged != null)
        {
            onActiveGunChanged(newActiveGun);
        }
        Debug.Log($"Switching to {newActiveGun.gunData.gunName} with ammo: {ammoData.currentAmmo}/{ammoData.totalAmmo}");
    }

    public GunBehaviour GetActiveGun()
    {
        return weapons[currentWeaponIndex].GetComponent<GunBehaviour>();
    }

    public void UpdateAmmoDisplay(){
        onActiveGunChanged?.Invoke(weapons[currentWeaponIndex].GetComponent<GunBehaviour>());
    }

    public void RefreshAmmoDisplayForAllGuns()
    {
        foreach (var weapon in weapons)
        {
            GunBehaviour gunBehaviour = weapon.GetComponent<GunBehaviour>();
            gunBehaviour.UpdateAmmoDisplay();
        }
    }

    public (int currentAmmo, int totalAmmo) GetAmmoDataForGun(Gun gun)
    {
        int index = weapons.FindIndex(w => w.GetComponent<GunBehaviour>().gunData == gun);
        return ammoStore.TryGetValue(index, out var ammoData) ? ammoData : (gun.ammoType.clipSize, gun.ammoType.startingMaxAmmo);
    }
}
