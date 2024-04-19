using DG.Tweening;
using System.Collections;
using UnityEngine;

public class GunBehaviour : MonoBehaviour
{
    public Gun gunData;

    private int currentAmmo;
    private int currentTotalAmmo;
    private float lastFireTime;
    private bool isReloading = false;

    private void Start()
    {
        currentAmmo = gunData.ammoType.clipSize;
        currentTotalAmmo = gunData.ammoType.startingMaxAmmo;
        Debug.Log($"{currentTotalAmmo} is the current total ammo.");
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > lastFireTime + gunData.ammoType.fireRate && currentAmmo > 0 && !isReloading)
        {
            Fire();
        }
        if (currentAmmo == 0 && currentTotalAmmo > 0)
        {
            StartCoroutine(Reload());
        }
    }

    private void Fire()
    {
        currentAmmo--;
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
        Debug.Log($"{gunData.gunName} is fired. Remaining Ammo is {currentAmmo}.");
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
        Debug.Log($"{gunData.name} is being reloaded. Current ammo is {currentAmmo}.");
        isReloading = true;
        float reloadTime = gunData.ammoType.reloadTime;
        yield return new WaitForSeconds(reloadTime);

        int neededAmmo = gunData.ammoType.clipSize - currentAmmo;
        int ammoToAdd = Mathf.Min(neededAmmo, currentTotalAmmo);
        currentAmmo += ammoToAdd;
        currentTotalAmmo -= ammoToAdd;

        isReloading = false;
        Debug.Log($"{gunData.name} is reloaded. Current ammo is {currentAmmo}.");
        Debug.Log($"{currentTotalAmmo} is the current total ammo.");
    }

    public void AddAmmo(int amount)
    {
        currentTotalAmmo = Mathf.Min(currentTotalAmmo + amount, gunData.ammoType.maxAmmoCapacity);
    }
}
