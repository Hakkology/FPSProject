using System.Collections;
using UnityEngine;

public class GunBehaviour : MonoBehaviour
{
    public Gun gunData; 
    private int currentAmmo; 
    private bool isReloading = false;

    private void Start()
    {
        currentAmmo = gunData.ammoType.clipSize;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && currentAmmo > 0 && !isReloading)
        {
            Fire();
        }

        if (currentAmmo == 0)
        {
            StartCoroutine(Reload());   
        }
    }

    private void Fire()
    {
        currentAmmo--; 
        // Raycast Shooting Mechanic
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {

            var damageable = hit.collider.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(gunData.gunAttackDamage);
            }

            // If the gun has pierce shot capability, continue the raycast through multiple objects
            if (gunData.gunPierceShot)
            {
                HandlePiercingShot(hit);
            }
        }
    }

    private void HandlePiercingShot(RaycastHit initialHit)
    {
        float distance = gunData.gunAttackDistance; // Maximum distance the bullet can travel
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
        // reloading
        isReloading = true;
        yield return new WaitForSeconds(gunData.ammoType.reloadTime);
        currentAmmo = gunData.ammoType.clipSize; 
        isReloading = false; 
    }
}
