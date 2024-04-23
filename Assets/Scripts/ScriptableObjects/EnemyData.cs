using UnityEngine;

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
    public int enemyFleeThreshold;
    public int enemyFleeDistance;

}