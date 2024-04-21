using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{

    public delegate void HealthChanged(int currentHealth, int maxHealth);
    public event HealthChanged onHealthChanged;

    public delegate void PlayerDied();
    public event PlayerDied onPlayerDied;

    [SerializeField] private PlayerData playerData;
    private int maxHealth;
    private int currentHealth;

    void Awake()
    {
        maxHealth = playerData.maxHealth;
        currentHealth = maxHealth;
    }

    void Start(){
        UpdateHPDisplay();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount; 
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);  
        UpdateHPDisplay();

        Debug.Log($"Player took {amount} damage. Current health: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player has died.");
        UpdatePlayerState();
        gameObject.SetActive(false);  
    }

    public void Heal(int amount)
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += amount;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            UpdateHPDisplay();

            Debug.Log($"Player healed by {amount}. Current health: {currentHealth}");
        }
    }

    public void UpdateHPDisplay(){
        onHealthChanged?.Invoke(currentHealth, maxHealth);
    }
    public void UpdatePlayerState(){
        onPlayerDied?.Invoke();
    }
    
}
