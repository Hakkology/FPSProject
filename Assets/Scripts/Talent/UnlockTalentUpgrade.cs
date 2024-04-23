using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnlockTalentUpgrade : MonoBehaviour
{
    public UnlockTalent talent;
    public TextMeshProUGUI talentNameText;
    public Toggle unlockToggle;

    protected virtual void Awake() => PopulateUI();
    protected virtual void Start() =>  UpdateUI();
    protected virtual void OnEnable() => unlockToggle.onValueChanged.AddListener(HandleUnlockToggle);
    protected virtual void OnDisable() => unlockToggle.onValueChanged.RemoveListener(HandleUnlockToggle);
    protected void UpdateUI()
    {
        unlockToggle.isOn = talent.isUnlocked;
        unlockToggle.interactable = true;
    }
    protected void PopulateUI() => talentNameText.text = talent.talentName;
    private void HandleUnlockToggle(bool isOn)
    {
        if (isOn && !talent.isUnlocked)
        {
            if (PlayerTalentController.Instance.TrySpendTalentPoints(talent.pointsToUnlock))
            {
                talent.Unlock();
                OnUnlockSuccess();
            }
            else
            {
                Debug.LogWarning("Not enough talent points to unlock!");
                unlockToggle.isOn = false;
            }
        }
        else if (!isOn && talent.isUnlocked)
        {
            talent.Lock();
            PlayerTalentController.Instance.RefundTalentPoints(talent.pointsToUnlock);
            OnLockSuccess();
        }
    }
    protected virtual void OnUnlockSuccess() => UpdateUI();
    protected virtual void OnLockSuccess() => UpdateUI();
    
}
