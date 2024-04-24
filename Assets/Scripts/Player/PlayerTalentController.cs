using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTalentController : MonoBehaviour
{
    public List<ScalableTalent> TalentReset = new List<ScalableTalent>();
    public static event Action OnTalentPointsChanged;
    public static event Action OnTalentLevelChanged;
    public static event Action OnKillCountChanged;
    public static event Action OnExperienceChanged;
    public static event Action OnLevelUp;
    
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

    public ExperienceLevels experienceLevels;
    public int currentLevel = 1;
    private int currentExperience = 0;
    public int CurrentExperience
    {
        get => currentExperience;
        private set
        {
            if (currentExperience != value)
            {
                currentExperience = value;
                Debug.Log($"CurrentExperience updated to: {currentExperience}");
                OnExperienceChanged?.Invoke();
            }
        }
    }
    private int talentPoints = 0;
    private int killScore;
    public int KillScore{
        get => killScore;
        private set
        {
            if (killScore != value)
            {
                killScore = value;
                OnKillCountChanged?.Invoke();
            }
        }
    }

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

    void Start(){
        ResetForNewGame();
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.E))
        {
            GainExperience(100);
        }
    }

    public void GainExperience(int xp)
    {
        currentExperience += xp;
        Debug.Log($"Player has earned {xp} amount of xp.");
        OnExperienceChanged?.Invoke();
        RegisterKill();
        CheckLevelUp();
    }
    public void RegisterKill()
    {
        KillScore++;
        Debug.Log($"New kill registered. Total kills: {KillScore}");
    }

    private void CheckLevelUp()
    {
        while (currentLevel < experienceLevels.experienceThresholds.Count &&
            currentExperience >= experienceLevels.GetExperienceForLevel(currentLevel))
        {
            currentLevel++;
            TalentPoints++;
            Debug.Log($"You are now Level {currentLevel}");
            OnLevelUp?.Invoke();
        }
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
    public void RefundTalentPoints(int pointsRefunded)
    {
        TalentPoints += pointsRefunded;
        OnTalentPointsChanged?.Invoke(); 
        Debug.Log($"Talent points refunded. You now have {TalentPoints} talent points.");
    }

    public void ResetForNewGame()
    {
        currentLevel = 1;
        currentExperience = 0;
        TalentPoints = 0;
        foreach (var talent in TalentReset)
        {
            talent.ResetLevel();
        }
        Debug.Log("Talent controller has been reset for a new game.");

        var playerHealth = GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.ResetHealth();
        }
    }
}
