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
        // Check for remaining ammo
        if (CurrentAmmo <= 0) return; // Exit if no ammo left
        CurrentAmmo--; // Decrement ammo count
        lastFireTime = Time.time; // Update time of last shot

        // Set up the raycast starting point and direction
        Vector3 rayStart = transform.position + transform.forward * 0.1f; // Start point just in front of the player to avoid self-collision
        Debug.DrawRay(rayStart, transform.forward * gunData.gunAttackDistance, Color.red, 2f); // Visualize the raycast for debugging

        // Use a sphere cast for a more forgiving hit detection
        int enemyLayerMask = LayerMask.GetMask("Enemy"); // Get the layer mask for enemies
        RaycastHit[] hits = Physics.SphereCastAll(rayStart, 0.5f, transform.forward, gunData.gunAttackDistance, enemyLayerMask);

        // Log the raycast call
        Debug.Log("Calling SphereCastAll with mask " + LayerMask.LayerToName(enemyLayerMask));

        foreach (var hit in hits)
        {
            // Log each hit for debugging
            Debug.Log($"Raycast hit: {hit.collider.gameObject.name}, Layer: {LayerMask.LayerToName(hit.collider.gameObject.layer)}");
            Debug.Log("Raycast start: " + rayStart + ", Direction: " + transform.forward);

            // Attempt to get the EnemyHealth component on the hit object
            EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                Debug.Log("EnemyHealth component found and not null");
                enemyHealth.TakeDamage((int)currentWeaponDamage); // Apply damage
                if (gunData.gunPierceShot.isUnlocked)
                {
                    HandlePiercingShot(hit); // Handle piercing shots if the feature is unlocked
                }
                break; // Stop at the first valid enemy hit if not piercing or continue based on your design
            }
            else
            {
                Debug.Log("EnemyHealth component not found on hit object.");
            }
        }

        Debug.Log($"{gunData.gunName} is fired. Remaining Ammo is {CurrentAmmo}.");

        if (!isReloading)
        {
            ApplyRecoil(); // Apply recoil effects
        }
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
