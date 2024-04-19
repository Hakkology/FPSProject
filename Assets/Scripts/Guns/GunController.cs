using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
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
            GameObject weapon = Instantiate(gun.gun, gunHolder.position, Quaternion.identity, gunHolder);
            GunBehaviour gunBehaviour = weapon.AddComponent<GunBehaviour>();
            gunBehaviour.gunData = gun;

            // corrections due to asset prefab
            if (gun.gunName == "Railgun") 
            {
                weapon.transform.localEulerAngles = new Vector3(0, 90, 0); 
                weapon.transform.localPosition = new Vector3(-0.6f, 0.25f, 0.15f);
                weapon.transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
            }
            weapon.SetActive(false); 
            weapons.Add(weapon);
        }

        weapons[currentWeaponIndex].SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwitchWeapon();
        }
    }

    void SwitchWeapon()
    {
        weapons[currentWeaponIndex].SetActive(false);
        currentWeaponIndex = (currentWeaponIndex + 1) % weapons.Count;
        weapons[currentWeaponIndex].SetActive(true);
    }
}
