using UnityEngine;
using DG.Tweening;

public class GameMenuPanelController : MonoBehaviour, IPanel
{
    public TalentPanelController talentPanelController;
    public GameObject panel; 
    private RectTransform rectTransform;
    private bool isPanelOpen = false;

    void Awake()
    {
        if (panel == null)
            panel = gameObject; 

        rectTransform = panel.GetComponent<RectTransform>();
        InitPanel(); 
    }

    void Update()
    {
        if (!talentPanelController.IsPanelOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            UpdatePanel(); 
        }
    }

    public void InitPanel()
    {
        panel.SetActive(false);
        rectTransform.anchoredPosition = new Vector2(0, rectTransform.rect.height);
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
        panel.SetActive(true);
        Time.timeScale = 0;

        rectTransform.DOKill();
        rectTransform.DOAnchorPosY(0, 0.5f)
            .SetEase(Ease.OutExpo)
            .SetUpdate(true); 
        
        isPanelOpen = true;
    }

    public void ExitPanel()
    {
        rectTransform.DOKill(true);
        rectTransform.DOAnchorPosY(rectTransform.rect.height, 0.5f) 
            .SetEase(Ease.InExpo)
            .SetUpdate(true) 
            .OnComplete(() => {
                panel.SetActive(false);
                Time.timeScale = 1; 
            });
        
        isPanelOpen = false;
    }

    public bool IsPanelOpen
    {
        get { return isPanelOpen; }
    }
}
