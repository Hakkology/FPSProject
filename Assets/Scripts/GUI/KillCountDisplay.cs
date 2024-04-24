using UnityEngine;
using TMPro; 

public class KillCountDisplay : MonoBehaviour
{
    public TextMeshProUGUI killCountText;

    void OnEnable()
    {
        PlayerTalentController.OnKillCountChanged += UpdateKillCountDisplay;
        UpdateKillCountDisplay();
    }

    void OnDisable()
    {
        PlayerTalentController.OnKillCountChanged -= UpdateKillCountDisplay;
    }

    private void UpdateKillCountDisplay()
    {
        if (killCountText == null)
        {
            Debug.LogError("KillCountText is not assigned!");
            return;
        }

        // Now accessing KillScore correctly
        killCountText.text = $"Kills: {PlayerTalentController.Instance.KillScore}";
    }
}
