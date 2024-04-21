using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TextMeshProUGUI healthText;

    private Coroutine healthAnimationCoroutine;
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
        healthText.text = $"{currentHealth} / {maxHealth}";
        healthSlider.maxValue = maxHealth;
        if (healthAnimationCoroutine != null)
            StopCoroutine(healthAnimationCoroutine);
        healthAnimationCoroutine = StartCoroutine(AnimateHealthChange(currentHealth));
    }

    private IEnumerator AnimateHealthChange(int targetHealth)
    {
        float preChangeHealth = healthSlider.value;
        float elapsed = 0f;
        float duration = 0.5f; 

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            healthSlider.value = Mathf.Lerp(preChangeHealth, targetHealth, elapsed / duration);
            yield return null;
        }

        healthSlider.value = targetHealth; 
    }
}
