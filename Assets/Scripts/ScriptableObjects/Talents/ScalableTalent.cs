using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Scalable Talent", menuName = "Talent System/Scalable Talent")]
public class ScalableTalent : ScriptableObject
{
    public event Action OnLevelChanged; 

    public string talentName;
    public int currentLevel;
    public int maxLevel;
    public int pointsPerLevel;
    public float[] valuesAtEachLevel;

    public float CurrentValue {
        get {
            if (currentLevel - 1 < valuesAtEachLevel.Length)
                return valuesAtEachLevel[currentLevel - 1];
            else
                return valuesAtEachLevel[maxLevel - 1]; 
        }
    }

    public void IncrementLevel() {
        if (currentLevel < maxLevel) {
            currentLevel++;
            OnLevelChanged?.Invoke();  // Trigger the event when the level changes
        }
    }
}