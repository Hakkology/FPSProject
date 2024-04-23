using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public EnemyData enemyData;
    private int maxHealth;
    private int currentHealth;

    void Awake() 
    {
        maxHealth = enemyData.enemyHealth;
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }
    }

    public int CurrentHealth
    {
        get { return currentHealth; }
    }
}
