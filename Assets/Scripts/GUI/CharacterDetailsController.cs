using TMPro;
using UnityEngine;

public class CharacterDetailsController : MonoBehaviour
{
    public GameObject player;
    
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI talentPointsText;
    public TextMeshProUGUI weaponText;
    public TextMeshProUGUI weaponDamageText;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI jumpText;

    private GunController gunController;
    private PlayerController playerController;
    private PlayerHealth playerHealth;
    private PlayerTalentController talentController;

    void Start(){

        gunController = player.GetComponentInChildren<GunController>();
        playerController = player.GetComponent<PlayerController>();
        playerHealth = player.GetComponent<PlayerHealth>();
        talentController = player.GetComponent<PlayerTalentController>();
        UpdateUI();
    }

    private void OnEnable()
    {
        PlayerEventDetails.Instance.OnPlayerDetailsChanged += UpdateUI;
    }

    private void OnDisable()
    {
        PlayerEventDetails.Instance.OnPlayerDetailsChanged += UpdateUI;
    }

    private void UpdateUI()
    {
        levelText.text = talentController.currentLevel.ToString();
        talentPointsText.text = talentController.TalentPoints.ToString();
        weaponText.text = gunController.GetCurrentGunName();
        weaponDamageText.text = gunController.GetCurrentGunDamage();
        hpText.text = playerHealth.GetMaxHealth();
        speedText.text = playerController.GetCurrentMovementSpeed();
        jumpText.text = playerController.GetCurrentJumpHeight();
    }
}
