using UnityEngine;

public class HealthItemBehaviour : MonoBehaviour
{
    public HealthPickupData healthData;
    public HealthPickupController healthController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.Heal(healthData.healAmount);
                gameObject.SetActive(false); 
                healthController.StartRespawnCoroutine(gameObject); 
            }
        }
    }
}
