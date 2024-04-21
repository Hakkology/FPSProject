using UnityEngine;
using TMPro;

public class HealthItemBehaviour : MonoBehaviour
{
    public HealthPickupData healthData;
    public TMP_Text hpDisplay;

    private void Start()
    {
        InitializeHp();
    }

    private void OnEnable()
    {
        InitializeHp();
    }

    private void InitializeHp()
    {
        UpdateHpDisplay();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth health = other.gameObject.GetComponent<PlayerHealth>();
            if (health != null)
            {
                int hpCollected = healthData.healAmount;
                health.Heal(hpCollected);
                UpdateHpDisplay();
                gameObject.SetActive(false);  
            }
        }
    }

    private void UpdateHpDisplay()
    {
        if (hpDisplay != null)
        {
            hpDisplay.text = healthData.healAmount.ToString();
        }
    }
}
