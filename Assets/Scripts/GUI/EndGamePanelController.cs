using UnityEngine;
using DG.Tweening;
using System.Collections;
using TMPro;

public class EndGamePanelController : MonoBehaviour, IPanel
{
    private PlayerHealth playerHealth;
    public GameObject panel; 
    public TextMeshProUGUI killText;
    private RectTransform rectTransform;
    private bool isPanelOpen = false;

    void Awake()
    {
        if (panel == null)
            panel = gameObject; 

        rectTransform = panel.GetComponent<RectTransform>();
        playerHealth = FindAnyObjectByType<PlayerHealth>();
        InitPanel(); 
    }

    void Start(){
        playerHealth.onPlayerDied += EnterPanel;
    }
    void OnDestroy(){
        playerHealth.onPlayerDied -= EnterPanel; 
    }

    public void InitPanel()
    {
        panel.SetActive(false);
        rectTransform.anchoredPosition = new Vector2(rectTransform.rect.width, 0);
        killText.text = "You have killed" + PlayerTalentController.Instance.KillScore.ToString() + "enemies!";
    }

    public void UpdatePanel()
    {
        if (!isPanelOpen)
            EnterPanel();
        else
            ExitPanel();
    }

    public void EnterPanel()
    {
        killText.text = "You have killed " + PlayerTalentController.Instance.KillScore.ToString() + " enemies!";
        panel.SetActive(true);
        Time.timeScale = 0;

        rectTransform.DOKill(); 
        rectTransform.DOAnchorPosX(0, 0.5f)
            .SetEase(Ease.OutExpo)
            .SetUpdate(true);
            CursorDisplay.Instance.RegisterPanelOpen();
        
        isPanelOpen = true;
    }

    public void ExitPanel()
    {
        Time.timeScale = 1;
        
        rectTransform.DOKill(true); 
        rectTransform.DOAnchorPosX(rectTransform.rect.width, 0.5f)
            .SetEase(Ease.InExpo)
            .SetUpdate(true) 
            .OnComplete(() => {
                Debug.Log("Panel moved off screen, now disabling.");
                panel.SetActive(false);
                Time.timeScale = 1; 
                CursorDisplay.Instance.RegisterPanelClose();
            });
        
        isPanelOpen = false;
    }

    public void RestartGame()
    {
        Time.timeScale = 1; 
        rectTransform.DOKill(true); 
        rectTransform.DOAnchorPosX(rectTransform.rect.width, 0.5f)
            .SetEase(Ease.InExpo)
            .SetUpdate(true) 
            .OnComplete(() => {
                Debug.Log("Panel moved off screen, now disabling.");
                panel.SetActive(false);
                CursorDisplay.Instance.RegisterPanelClose();
            });
        isPanelOpen = false;
        StartCoroutine(ExitingAnimation());
    }

    IEnumerator ExitingAnimation(){
        yield return new WaitForSeconds(0.3f);
        PlayerTalentController.Instance.ResetForNewGame();
        SceneHandler.Instance.StartGame();
    }

    public bool IsPanelOpen
    {
        get { return isPanelOpen; }
    }
}
