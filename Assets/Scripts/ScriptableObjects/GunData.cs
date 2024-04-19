using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "Inventory/Gun")]
public class Gun : ScriptableObject
{
    public GameObject gun;
    public AmmoType ammoType;
    public string gunName;
    public int gunAttackDamage;
    public int gunAttackDistance;
    public bool gunPierceShot;
}