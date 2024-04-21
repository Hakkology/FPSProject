using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

public class GunBehaviour : MonoBehaviour
{
    public Gun gunData;

    public delegate void AmmoChanged(int currentAmmo, int maxAmmo);
    public event AmmoChanged onAmmoChanged;

    public int CurrentAmmo { get; private set; }
    public int CurrentTotalAmmo { get; private set; }
    
    private float lastFireTime;
    private bool isReloading = false;

    private void Start()
    {
        CurrentAmmo = gunData.ammoType.clipSize;
        CurrentTotalAmmo = gunData.ammoType.startingMaxAmmo;
        Debug.Log($"{CurrentTotalAmmo} is the current total ammo.");
        UpdateAmmoDisplay();
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > lastFireTime + gunData.ammoType.fireRate && CurrentAmmo > 0 && !isReloading)
        {
            Fire();
            UpdateAmmoDisplay();
        }
        if (CurrentAmmo == 0 && CurrentTotalAmmo > 0)
        {
            StartCoroutine(Reload());
        }
    }

    private void Fire()
    {
        CurrentAmmo--;
        lastFireTime = Time.time;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, gunData.gunAttackDistance))
        {
            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.yellow, 1f); 

            var damageable = hit.collider.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(gunData.gunAttackDamage);
            }

            if (gunData.gunPierceShot)
            {
                HandlePiercingShot(hit);
            }
        }
        else
        {
            // If the raycast didn't hit anything, draw the ray to the maximum attack distance
            Debug.DrawRay(transform.position, transform.forward * gunData.gunAttackDistance, Color.red, 1f); 
        }
        Debug.Log($"{gunData.gunName} is fired. Remaining Ammo is {CurrentAmmo}.");
    }


    private void HandlePiercingShot(RaycastHit initialHit)
    {
        float distance = gunData.gunAttackDistance;
        Ray ray = new Ray(initialHit.point + transform.forward * 0.01f, transform.forward);
        RaycastHit[] hits = Physics.RaycastAll(ray, distance);

        foreach (RaycastHit hit in hits)
        {
            var damageable = hit.collider.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(gunData.gunAttackDamage);
            }
        }
    }

    private IEnumerator Reload()
    {
        Debug.Log($"{gunData.name} is being reloaded. Current ammo is {CurrentAmmo}.");
        isReloading = true;
        float reloadTime = gunData.ammoType.reloadTime;
        yield return new WaitForSeconds(reloadTime);

        int neededAmmo = gunData.ammoType.clipSize - CurrentAmmo;
        int ammoToAdd = Mathf.Min(neededAmmo, CurrentTotalAmmo);
        CurrentAmmo += ammoToAdd;
        CurrentTotalAmmo -= ammoToAdd;

        isReloading = false;
        UpdateAmmoDisplay();

        Debug.Log($"{gunData.name} is reloaded. Current ammo is {CurrentAmmo}.");
        Debug.Log($"{CurrentTotalAmmo} is the current total ammo.");
    }

    public int AddAmmo(int amount)
    {
        int ammoNeeded = gunData.ammoType.maxAmmoCapacity - CurrentTotalAmmo;
        int ammoToCollect = Mathf.Min(ammoNeeded, amount);
        
        CurrentTotalAmmo += ammoToCollect;
        UpdateAmmoDisplay();
        
        Debug.Log($"{gunData.gunName} current total ammo was {CurrentTotalAmmo}.");
        Debug.Log($"{gunData.gunName} picked up {ammoToCollect} ammo. Current total ammo: {CurrentTotalAmmo}.");
        
        return ammoToCollect;
    }

    private void UpdateAmmoDisplay()
    {
        onAmmoChanged?.Invoke(CurrentAmmo, CurrentTotalAmmo);
    }
}
