using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public EnemyData enemyData;
    private Transform targetTransform; 

    public void Initialize(Transform target)
    {
        targetTransform = target; 
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null) {
            rb.velocity = (target.position - transform.position).normalized * enemyData.enemyProjectileSpeed;
        }
    }

    void Update()
    {

        if (Vector3.Distance(transform.position, targetTransform.position) < 0.1f)
        {
            HitTarget();
        }
    }

    private void HitTarget()
    {
        IDamageable damageable = targetTransform.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(enemyData.enemyAttackDamage);
        }
        Debug.Log($"Projectile hit the target for {enemyData.enemyAttackDamage} damage.");
    }
}
