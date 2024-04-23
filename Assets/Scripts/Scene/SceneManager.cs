using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    private static SceneHandler instance;

    public static SceneHandler Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<SceneHandler>();
                if (instance == null)
                {
                    GameObject go = new GameObject();
                    go.name = "SceneManager";
                    instance = go.AddComponent<SceneHandler>();
                    DontDestroyOnLoad(go);
                }
            }
            return instance;
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void StartMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("FPScene");
    }
    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; 
        #else
            Application.Quit(); 
        #endif
    }
}
