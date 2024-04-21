using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "Inventory/Gun")]
public class Gun : ScriptableObject
{
    public string gunName;
    public GameObject gun;
    public AmmoType ammoType;

    public int gunAttackDamage;
    public int gunAttackDistance;

    // recoil
    public float recoilDuration;
    public float recoilValues;
    public bool gunPierceShot;

    // Just to make sure that the assets are instantiated at the right location.
    // Origin positions are a bit off.
    public Vector3 customPosition;
}