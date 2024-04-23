using UnityEngine;

public enum AttackType
{
    Melee,
    Ranged
}

[CreateAssetMenu(fileName = "New Enemy", menuName = "Character/Enemy")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public int enemyHealth;
    public int enemyAttackDamage;
    public int enemyExperiencePoints;
    public float enemySightRange;
    public int enemyPatrolRange;
    public int enemyWalkSpeed;
    public int enemyRunSpeed;
    public int enemyAttackRange;
    public float enemyAttackCooldown;
    public GameObject enemyProjectilePrefab;
    public int enemyProjectileSpeed;
    public int enemyFleeThreshold;
    public int enemyFleeDistance;
    public AttackType attackType;
    public int enemyPoolSize;
    public int enemySpawnCooldown;

}