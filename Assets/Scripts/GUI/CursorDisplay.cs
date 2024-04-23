using UnityEngine;
using UnityEngine.UI;

public class CursorDisplay : MonoBehaviour
{
    public static CursorDisplay Instance { get; private set; }

    [SerializeField] private GameObject crosshairTexture;
    private int openPanelCount = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterPanelOpen()
    {
        openPanelCount++;
        UpdateCursorState();
    }

    public void RegisterPanelClose()
    {
        openPanelCount--;
        UpdateCursorState();
    }

    private void UpdateCursorState()
    {
        bool isAnyPanelOpen = openPanelCount > 0;

        if (crosshairTexture != null)
        {
            crosshairTexture.SetActive(!isAnyPanelOpen);
        }

        Cursor.lockState = isAnyPanelOpen ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isAnyPanelOpen;
    }
}
