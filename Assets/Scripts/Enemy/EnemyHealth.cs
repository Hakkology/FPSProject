using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable, IExperience
{
    public EnemyData enemyData;
    private int maxHealth;
    private int currentHealth;
    public event Action<int> OnHealthChanged;

    void Awake() 
    {
        maxHealth = enemyData.enemyHealth;
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log($"{enemyData.enemyName} took {amount} damage. Current Health is {currentHealth}");
        OnHealthChanged?.Invoke(currentHealth);
        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }
    }

    public void ResetHealth(){
        maxHealth = enemyData.enemyHealth;
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(currentHealth);
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
