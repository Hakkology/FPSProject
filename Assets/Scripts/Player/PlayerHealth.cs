using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount; 
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);  

        Debug.Log($"Player took {amount} damage. Current health: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player has died.");
        gameObject.SetActive(false);  
    }

    public void Heal(int amount)
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += amount;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            Debug.Log($"Player healed by {amount}. Current health: {currentHealth}");
        }
    }
}
