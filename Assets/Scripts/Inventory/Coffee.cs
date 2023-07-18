using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffee : MonoBehaviour, IWeapon
{
    [SerializeField] WeaponInfoSO weaponInfoSO;

    public WeaponInfoSO GetWeaponInfo() => weaponInfoSO;
    public void Attack()
    {
        Debug.Log("Coffee attacked");
    }
}
