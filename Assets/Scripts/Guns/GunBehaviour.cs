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
    private float currentWeaponDamage;

    void Start() {
        if (gunData != null && gunData.gunAttackDamage != null) {
            gunData.gunAttackDamage.OnLevelChanged += UpdateWeaponDamage;
            UpdateWeaponDamage(); 
        }
    }

    void OnDestroy() {
        if (gunData != null && gunData.gunAttackDamage != null) {
            gunData.gunAttackDamage.OnLevelChanged -= UpdateWeaponDamage;
        }
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
        if (Input.GetKeyDown(KeyCode.R) && CurrentAmmo < gunData.ammoType.clipSize && CurrentTotalAmmo > 0 && !isReloading)
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
                damageable.TakeDamage((int)currentWeaponDamage);
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

        ApplyRecoil();
        
    }

    private void ApplyRecoil()
    {
        // recoil dotween
        transform.DOComplete(); 
        transform.DOLocalMoveZ(-gunData.recoilValues, gunData.recoilDuration).SetRelative().SetEase(Ease.OutCubic).OnComplete(() => {
            transform.DOLocalMoveZ(gunData.recoilValues, gunData.recoilDuration / 2).SetRelative().SetEase(Ease.InCubic);
        });
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
                damageable.TakeDamage((int)gunData.gunAttackDamage.CurrentValue);
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
        int ammoNeeded = (int)gunData.ammoType.maxAmmoCapacity.CurrentValue - CurrentTotalAmmo;
        int ammoToCollect = Mathf.Min(ammoNeeded, amount);
        
        CurrentTotalAmmo += ammoToCollect;
        UpdateAmmoDisplay();
        
        Debug.Log($"{gunData.gunName} current total ammo was {CurrentTotalAmmo}.");
        Debug.Log($"{gunData.gunName} picked up {ammoToCollect} ammo. Current total ammo: {CurrentTotalAmmo}.");
        
        return ammoToCollect;
    }

    public void SetAmmo(int currentAmmo, int totalAmmo)
    {
        CurrentAmmo = currentAmmo;
        CurrentTotalAmmo = totalAmmo;
        UpdateAmmoDisplay();
    }

    public void UpdateAmmoDisplay()
    {
        onAmmoChanged?.Invoke(CurrentAmmo, CurrentTotalAmmo);
    }

    private void UpdateWeaponDamage() {
        currentWeaponDamage = gunData.gunAttackDamage.CurrentValue;
        Debug.Log($"Updated gun damage for {gunData.gunName} to {currentWeaponDamage}");
    }

    public float GetCurrentDamage(){
        return currentWeaponDamage;
    }
}
