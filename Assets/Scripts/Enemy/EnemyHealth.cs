using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable, IExperience
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
            GiveExperience(enemyData.enemyExperiencePoints);
        }
    }

    public void GiveExperience(int xp)
    {
        PlayerTalentController.Instance.GainExperience(xp);
    }

    public int CurrentHealth
    {
        get { return currentHealth; }
    }
}
