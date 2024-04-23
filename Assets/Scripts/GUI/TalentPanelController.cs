using UnityEngine;
using DG.Tweening; // Import the DoTween namespace

public class TalentPanelController : MonoBehaviour, IPanel
{
    public GameMenuPanelController gameMenuPanelController;
    public GameObject panel; 
    private RectTransform rectTransform;
    private bool isPanelOpen;

    void Awake()
    {
        if (panel == null)
            panel = gameObject; 

        rectTransform = panel.GetComponent<RectTransform>();
        InitPanel(); 
    }

    void Update()
    {
        if (!gameMenuPanelController.IsPanelOpen && Input.GetKeyDown(KeyCode.Tab))
        {
            UpdatePanel(); 
        }
        if (IsPanelOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            UpdatePanel();
        }
    }

    public void InitPanel()
    {
        panel.SetActive(false);
        rectTransform.anchoredPosition = new Vector2(-rectTransform.rect.width, 0);
        isPanelOpen = false;
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
        rectTransform.DOAnchorPosX(-rectTransform.rect.width, 0.5f)
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

    public bool IsPanelOpen
    {
        get { return isPanelOpen; }
    }

}
