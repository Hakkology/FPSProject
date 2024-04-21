using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerData", menuName = "Game Data/Player Data")]
public class PlayerData : ScriptableObject
{
    public float jumpHeight;
    public float walkSpeed;
    public float runSpeed;
    public float mouseSensitivity;
    public int maxHealth;
}