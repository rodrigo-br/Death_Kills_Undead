using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : MonoBehaviour, IWeapon
{
    [SerializeField] WeaponInfoSO weaponInfoSO;
    [SerializeField] GameObject magicLaserPrefab;
    [SerializeField] Transform magicLaserSpawnPoint;
    Animator myAnimator;
    readonly int ATTACK_HASH = Animator.StringToHash(StringsDefinitions.ATTACK);

    void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }

    public WeaponInfoSO GetWeaponInfo() => weaponInfoSO;

    public void Attack()
    {
        myAnimator.SetTrigger(ATTACK_HASH);
    }

    public void SpawnMagicLaserAnimEvent()
    {
        GameObject newArrow = Instantiate(magicLaserPrefab, magicLaserSpawnPoint.position, Quaternion.identity);
    }

    void FixedUpdate()
    {
        AdjustWeaponFacingDirection();
    }

    void AdjustWeaponFacingDirection()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.parent.parent.position);

        float left = mousePos.x < playerScreenPoint.x ? -180 : 0;
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        transform.parent.rotation = Quaternion.Euler(0, left, angle);
    }
}
