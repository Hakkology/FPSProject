using UnityEngine;

public class HealthTalentUpgrade : TalentUpgrade
{
    [SerializeField] private PlayerHealth playerHealth;

    protected override void OnUpgradeSuccess()
    {
        base.OnUpgradeSuccess();
        playerHealth.UpdateMaxHealth(); 
    }
}
