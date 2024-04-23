using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;
using System.Collections;

public class MainMenuPanelController : MonoBehaviour, IPanel
{
    public Button[] buttons;
    public GameObject panel; 
    private RectTransform rectTransform;

    private Coroutine AnimationHandler;

    void Awake()
    {
        if (panel == null)
            panel = gameObject; 

        rectTransform = panel.GetComponent<RectTransform>();
        InitPanel(); 
    }

    public void InitPanel()
    {

    }

    private void SetButtonsInteractable(bool interactable)
    {
        foreach (Button btn in buttons)
        {
            btn.interactable = interactable;
        }
    }

    public void UpdatePanel()
    {

    }

    public void ExitPanel()
    {
        SetButtonsInteractable(false);
        rectTransform.DOAnchorPosY(rectTransform.rect.height*2, 0.5f)
            .SetEase(Ease.InExpo)
            .SetUpdate(true)
            .OnComplete(() => 
            {
                StartCoroutine(WaitForAnimation());
            });
    }

    IEnumerator WaitForAnimation(){

        yield return new WaitForSecondsRealtime(.3f);
        SceneHandler.Instance.StartGame();
    }
}
