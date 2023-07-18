using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] WeaponInfoSO weaponInfoSO;

    public WeaponInfoSO GetWeaponInfo()
    {
        return weaponInfoSO;
    }
}
