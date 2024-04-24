using UnityEngine;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;
    public static SoundManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SoundManager>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject("SoundManager");
                    _instance = obj.AddComponent<SoundManager>();
                }
            }
            return _instance;
        }
    }

    public List<AudioClip> sounds; // Public list to add sounds via Inspector
    private Dictionary<string, AudioClip> soundLibrary;
    public float volume = 0.5f; // Default volume level

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
            InitializeSounds();
        }
    }

    private void InitializeSounds()
    {
        soundLibrary = new Dictionary<string, AudioClip>();
        foreach (AudioClip clip in sounds)
        {
            if (!soundLibrary.ContainsKey(clip.name))
            {
                soundLibrary.Add(clip.name, clip);
            }
        }
    }

    public void PlaySFX(string soundName)
    {
        if (soundLibrary.TryGetValue(soundName, out AudioClip clip))
        {
            AudioSource.PlayClipAtPoint(clip, Vector3.zero, volume); // Play at the center of the world with controlled volume
        }
        else
        {
            Debug.LogError("Sound not found: " + soundName);
        }
    }
}
