using UnityEngine;
using TMPro;

public class HealthItemBehaviour : MonoBehaviour
{
    public HealthPickupData healthData;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth health = other.gameObject.GetComponent<PlayerHealth>();
            if (health != null)
            {
                int hpCollected = healthData.healAmount;
                health.Heal(hpCollected);
                gameObject.SetActive(false);  
            }
        }
    }

}
