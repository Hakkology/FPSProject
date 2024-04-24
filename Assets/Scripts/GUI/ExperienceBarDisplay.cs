using TMPro;
using UnityEngine;
using UnityEngine.UI; 

public class ExperienceBarDisplay : MonoBehaviour
{
    public Slider xpSlider; 
    public TextMeshProUGUI levelText;

    void Start(){
        PlayerTalentController.OnExperienceChanged += UpdateExperienceDisplay;
        PlayerTalentController.OnLevelUp += AdjustLevelThresholds;
        Debug.Log("Subscribed to PlayerTalentController events.");
        InitializeSlider();
    }

    void OnDisable()
    {
        PlayerTalentController.OnExperienceChanged -= UpdateExperienceDisplay;
        PlayerTalentController.OnLevelUp -= AdjustLevelThresholds;
        Debug.Log("Unsubscribed from PlayerTalentController events.");
    }

    private void InitializeSlider()
    {
        if (PlayerTalentController.Instance != null)
        {
            Debug.Log("Initializing slider with current XP and level thresholds.");
            UpdateExperienceDisplay();
            AdjustLevelThresholds();
        }
        else
        {
            Debug.LogError("PlayerTalentController instance is null during slider initialization.");
        }
    }

    private void UpdateExperienceDisplay()
    {
        int currentLevel = PlayerTalentController.Instance.currentLevel;
        int experienceForCurrentLevel = (currentLevel > 1) ? PlayerTalentController.Instance.experienceLevels.GetExperienceForLevel(currentLevel-1) : 0;
        int experienceForNextLevel = PlayerTalentController.Instance.experienceLevels.GetExperienceForLevel(currentLevel);
        
        xpSlider.minValue = 0;
        xpSlider.maxValue = experienceForNextLevel - experienceForCurrentLevel;
        xpSlider.value = PlayerTalentController.Instance.CurrentExperience - experienceForCurrentLevel;

        levelText.text = "Level:" + currentLevel.ToString();

        Debug.Log($"Slider updated: Min={xpSlider.minValue}, Max={xpSlider.maxValue}, Current XP={PlayerTalentController.Instance.CurrentExperience}, Value={xpSlider.value}");
    }


    private void AdjustLevelThresholds()
    {
        if (xpSlider != null && PlayerTalentController.Instance != null)
        {
            UpdateExperienceDisplay();
            Debug.Log("Adjusted slider thresholds based on new level.");
        }
    }

    // Ensure this method is triggered properly
    public void OnLevelUp()
    {
        AdjustLevelThresholds();  
        Debug.Log("Level up detected, adjusting slider.");
    }
}
