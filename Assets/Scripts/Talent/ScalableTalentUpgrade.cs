using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScalableTalentUpgrade : MonoBehaviour
{
    public ScalableTalent talent;
    public TextMeshProUGUI talentNameText;
    public TextMeshProUGUI talentLevelText;
    public Button incrementButton;
    public Button decrementButton;

    protected virtual void Awake() => PopulateUI();
    protected virtual void Start() => UpdateUI();
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

    protected void UpdateUI() => talentLevelText.text = $"{talent.currentLevel}";

    protected void PopulateUI(){
        talentNameText.text = talent.talentName;
        incrementButton.onClick.AddListener(AttemptUpgrade);
        decrementButton.onClick.AddListener(AttemptDecrease);
    }

    protected void UpdateInteractability()
    {
        incrementButton.interactable = PlayerTalentController.Instance.TalentPoints >= talent.pointsPerLevel && talent.currentLevel < talent.maxLevel;
        decrementButton.interactable = talent.currentLevel > 1;
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

    protected void AttemptDecrease()
    {
        if (talent.currentLevel > 1)
        {
            talent.DecrementLevel();
            PlayerTalentController.Instance.RefundTalentPoints(talent.pointsPerLevel); // Refund points after decreasing level
            UpdateUI();
            OnDecreaseSuccess();
        }
        else
        {
            Debug.LogWarning("Minimum level reached!");
        }
    }

    protected virtual void OnUpgradeSuccess() => PlayerEventDetails.Instance.TriggerCharacterDetailsChanged();
    protected virtual void OnDecreaseSuccess() => PlayerEventDetails.Instance.TriggerCharacterDetailsChanged();
    
}
