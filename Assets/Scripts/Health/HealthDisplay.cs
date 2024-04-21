using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private Slider healthSlider;


    void Awake()
    {
        if (playerHealth == null)
        {
            playerHealth = FindAnyObjectByType<PlayerHealth>();
            if (playerHealth == null)
            {
                Debug.LogError("PlayerHealth component not found in the scene.");
                return;
            }
        }
    }
    void OnEnable()
    {
        playerHealth.onHealthChanged += UpdateHealthBar;
    }

    void OnDisable()
    {
        playerHealth.onHealthChanged -= UpdateHealthBar;
    }

    private void UpdateHealthBar(int currentHealth, int maxHealth)
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }
}
