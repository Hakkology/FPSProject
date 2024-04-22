using UnityEngine;
using UnityEngine.UI;

public class TalentUpgrade : MonoBehaviour
{
    public ScalableTalent talent;
    public Text levelText;
    public Button upgradeButton;

    protected virtual void Start()
    {
        upgradeButton.onClick.AddListener(AttemptUpgrade);
        UpdateUI();
    }

    protected virtual void OnEnable()
    {
        PlayerTalentController.OnTalentPointsChanged += UpdateInteractability;
        PlayerTalentController.OnTalentLevelChanged += UpdateInteractability;
        UpdateInteractability();
    }

    protected virtual void OnDisable()
    {
        PlayerTalentController.OnTalentPointsChanged -= UpdateInteractability;
        PlayerTalentController.OnTalentLevelChanged -= UpdateInteractability;
    }

    protected void UpdateUI()
    {
        levelText.text = $"Level: {talent.currentLevel}";
    }

    protected void UpdateInteractability()
    {
        upgradeButton.interactable = PlayerTalentController.Instance.TalentPoints >= talent.pointsPerLevel && talent.currentLevel < talent.maxLevel;
    }

    protected void AttemptUpgrade()
    {
        if (PlayerTalentController.Instance.TrySpendTalentPoints(talent.pointsPerLevel))
        {
            talent.IncrementLevel();
            UpdateUI();
            OnUpgradeSuccess();
        }
        else
        {
            Debug.LogWarning("Not enough talent points!");
        }
    }

    protected virtual void OnUpgradeSuccess()
    {
        
    }
}
