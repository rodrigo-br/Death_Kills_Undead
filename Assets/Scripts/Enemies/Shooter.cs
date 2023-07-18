using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour, IWeapon
{
    [SerializeField] WeaponInfoSO myWeaponInfoSO;
    GameObject bulletPrefab;

    public WeaponInfoSO GetWeaponInfo() => myWeaponInfoSO;

    void Awake()
    {
        bulletPrefab = myWeaponInfoSO.weaponPrefab;
    }

    public void Attack()
    {
        Vector2 targetDirection = PlayerController.Instance.transform.position - transform.position;

        GameObject newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        newBullet.transform.right = targetDirection;
    }

}
