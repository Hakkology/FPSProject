using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ExperienceLevels", menuName = "Game Configuration/Experience Levels")]
public class ExperienceLevels : ScriptableObject
{
    public List<int> experienceThresholds;
    public int GetExperienceForLevel(int level)
    {
        if (level - 1 < experienceThresholds.Count)
        {
            return experienceThresholds[level - 1];
        }
        else
        {
            Debug.LogError("Level requested exceeds configured experience thresholds.");
            return 0;  
        }
    }
}
