using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerTalentController : MonoBehaviour
{
    public static event Action OnTalentPointsChanged;
    public static event Action OnTalentLevelChanged;
    
    private static PlayerTalentController _instance;
    public static PlayerTalentController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<PlayerTalentController>();
                if (_instance == null)
                {
                    Debug.LogError("PlayerTalentController instance not found, creating a new one.");
                    GameObject singletonObject = new GameObject("PlayerTalentController");
                    _instance = singletonObject.AddComponent<PlayerTalentController>();
                }
            }
            return _instance;
        }
    }

    [SerializeField] private ExperienceLevels experienceLevels;
    private int currentLevel = 1;
    private int currentExperience = 0;
    private int talentPoints = 0;
    public int TalentPoints
    {
        get => talentPoints;
        private set
        {
            if (talentPoints != value)
            {
                talentPoints = value;
                OnTalentPointsChanged?.Invoke();
            }
        }
    }

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.E))
        {
            GainExperience(200);
        }
    }

    public void GainExperience(int xp)
    {
        currentExperience += xp;
        CheckLevelUp();
    }

    private void CheckLevelUp()
    {
        while (currentLevel < experienceLevels.experienceThresholds.Count &&
               currentExperience >= experienceLevels.GetExperienceForLevel(currentLevel + 1))
        {
            currentExperience -= experienceLevels.GetExperienceForLevel(currentLevel + 1);
            currentLevel++;
            TalentPoints++;  
            Debug.Log($"You are now Level {currentLevel}");
            Debug.Log($"You have {talentPoints} amount of talent points.");
            OnLevelUp();
        }
    }

    private void OnLevelUp()
    {
        
    }

    public bool TrySpendTalentPoints(int pointsNeeded)
    {
        if (TalentPoints >= pointsNeeded)
        {
            TalentPoints -= pointsNeeded;
            OnTalentLevelChanged?.Invoke();
            Debug.Log($"You have {talentPoints} amount of talent points.");
            return true;
        }
        return false;
        
    }

    public void ResetForNewGame()
    {
        currentLevel = 1;
        currentExperience = 0;
        TalentPoints = 0;
        Debug.Log("Talent controller has been reset for a new game.");
    }
}
