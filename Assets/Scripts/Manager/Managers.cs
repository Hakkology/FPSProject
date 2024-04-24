using UnityEngine;

public class Managers : MonoBehaviour
{
    [SerializeField] private SoundManager soundManager;
    public static SoundManager SoundManager => Instance.soundManager;


    [SerializeField] private SceneHandler sceneHandler;
    public static SceneHandler SceneHandler => Instance.sceneHandler;

    private static Managers _instance;
    public static Managers Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<Managers>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject("Managers");
                    _instance = obj.AddComponent<Managers>();
                }
                DontDestroyOnLoad(_instance.gameObject); 
            }
            return _instance;
        }
    }


    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject); 
        }
    }
}
