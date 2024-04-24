using TMPro;
using UnityEngine;
using UnityEngine.UI; 

public class ExperienceBarDisplay : MonoBehaviour
{
    public Slider xpSlider; 
    public TextMeshProUGUI levelText;

    void Start(){
        PlayerTalentController.OnExperienceChanged += UpdateExperienceDisplay;
        PlayerTalentController.OnLevelUp += UpdateExperienceDisplay;
        Debug.Log("Subscribed to PlayerTalentController events.");
        InitializeSlider();
    }

    void OnDisable()
    {
        PlayerTalentController.OnExperienceChanged -= UpdateExperienceDisplay;
        PlayerTalentController.OnLevelUp -= UpdateExperienceDisplay;
        Debug.Log("Unsubscribed from PlayerTalentController events.");
    }

    private void InitializeSlider()
    {
        if (PlayerTalentController.Instance != null)
        {
            Debug.Log("Initializing slider with current XP and level thresholds.");
            UpdateExperienceDisplay();
        }
        else
        {
            Debug.LogError("PlayerTalentController instance is null during slider initialization.");
        }
    }

    private void UpdateExperienceDisplay()
    {
        int currentLevel = PlayerTalentController.Instance.currentLevel;
        int experienceForCurrentLevel = (currentLevel > 1) ? 
            PlayerTalentController.Instance.experienceLevels.GetExperienceForLevel(currentLevel - 1) : 0;
        int experienceForNextLevel = PlayerTalentController.Instance.experienceLevels.GetExperienceForLevel(currentLevel);
        
        xpSlider.minValue = experienceForCurrentLevel;
        xpSlider.maxValue = experienceForNextLevel;
        int sliderValue = PlayerTalentController.Instance.CurrentExperience;
        xpSlider.value = Mathf.Max(0, sliderValue);

        Debug.Log($"Level: {currentLevel}, Current XP: {PlayerTalentController.Instance.CurrentExperience}, Min XP: {experienceForCurrentLevel}, Max XP: {experienceForNextLevel}, Slider Value: {xpSlider.value}");
        levelText.text = "Level:" + $"{currentLevel}";    
    }

    // Ensure this method is triggered properly
    public void OnLevelUp()
    {
        UpdateExperienceDisplay();
        Debug.Log("Level up detected, adjusting slider.");
    }
}
