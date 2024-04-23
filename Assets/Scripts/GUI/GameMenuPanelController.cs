using UnityEngine;
using DG.Tweening;
using System.Collections;

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

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        rectTransform.DOKill();
        rectTransform.DOAnchorPosY(0, 0.5f)
            .SetEase(Ease.OutExpo)
            .SetUpdate(true); 
        
        isPanelOpen = true;
    }

    public void ExitPanel()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

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

    public void ExitGame()
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
        StartCoroutine(ExitingAnimation());
    }

    IEnumerator ExitingAnimation(){
        yield return new WaitForSeconds(0.3f);
        SceneHandler.Instance.StartMainMenu();
    }

    public bool IsPanelOpen
    {
        get { return isPanelOpen; }
    }
}
