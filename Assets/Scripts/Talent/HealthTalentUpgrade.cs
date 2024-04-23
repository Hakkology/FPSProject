using UnityEngine;

public class HealthTalentUpgrade : TalentUpgrade
{
    [SerializeField] private PlayerHealth playerHealth;

    protected override void OnUpgradeSuccess()
    {
        base.OnUpgradeSuccess();
        playerHealth.UpdateMaxHealth(); 
    }
    protected override void OnDecreaseSuccess()
    {
        base.OnDecreaseSuccess();
        playerHealth.UpdateMaxHealth(); 
    }
}
