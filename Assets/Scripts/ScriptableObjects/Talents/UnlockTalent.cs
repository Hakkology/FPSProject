using UnityEngine;

[CreateAssetMenu(fileName = "New UnlockTalent", menuName = "Talent System/Unlock Talent")]
public class UnlockTalent : ScriptableObject
{
    public string talentName;
    public bool isUnlocked;
    public int pointsToUnlock;

    public void Unlock() {
        if (!isUnlocked) {
            isUnlocked = true;
        }
    }
}
