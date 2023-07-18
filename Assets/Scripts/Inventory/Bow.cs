using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour, IWeapon
{
    [SerializeField] WeaponInfoSO weaponInfoSO;
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] Transform arrowSpawnPoint;
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
        GameObject newArrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, transform.rotation);
    }

    void FixedUpdate()
    {
        AdjustWeaponFacingDirection();
    }

    void AdjustWeaponFacingDirection()
    {
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.parent.parent.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        transform.parent.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
