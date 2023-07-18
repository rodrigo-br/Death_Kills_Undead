using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Weapon", fileName = "NewWeaponInfoSO")]
public class WeaponInfoSO : ScriptableObject
{
    public GameObject weaponPrefab;
    public float weaponCooldown;
}
