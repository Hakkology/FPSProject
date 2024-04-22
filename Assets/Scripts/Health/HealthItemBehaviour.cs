using UnityEngine;

public class HealthItemBehaviour : MonoBehaviour
{
    public HealthPickupData healthData;
    public HealthItemController healthController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.Heal(healthData.healAmount);
                if (healthController != null)
                {
                    healthController.EnqueueItem(gameObject);
                    healthController.DecreaseActivePickups();
                }
            }
        }
    }
}
