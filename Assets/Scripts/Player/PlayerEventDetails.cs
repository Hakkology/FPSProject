using System;
using UnityEngine;

public class PlayerEventDetails : MonoBehaviour
{
    public static PlayerEventDetails Instance { get; private set; }
    public event Action OnPlayerDetailsChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void TriggerCharacterDetailsChanged()
    {
        OnPlayerDetailsChanged?.Invoke();
    }
}
