using UnityEngine;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    public List<AudioClip> sounds;
    private Dictionary<string, AudioClip> soundLibrary;
    public float volume = 0.5f;

    private AudioSource musicSource;

    void Start()
    {
        musicSource = gameObject.AddComponent<AudioSource>(); // Add an AudioSource component dynamically
        musicSource.loop = true; 
        musicSource.volume = volume; 

        InitializeSounds();
        PlayMusic("Music"); 
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
            AudioSource.PlayClipAtPoint(clip, Vector3.zero, volume); 
        }
        else
        {
            Debug.LogError("Sound not found: " + soundName);
        }
    }

    public void PlayMusic(string musicName)
    {
        if (soundLibrary.TryGetValue(musicName, out AudioClip clip))
        {
            musicSource.clip = clip;
            musicSource.Play();
        }
        else
        {
            Debug.LogError("Music not found: " + musicName);
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}

