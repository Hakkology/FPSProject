using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Character/Enemy")]
public class Enemy : ScriptableObject
{
    public int healthAmount;
    public int attackDamage;
    public int experiencePoints;
}