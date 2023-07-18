using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeapon : Singleton<ActiveWeapon>
{
    public MonoBehaviour CurrentActiveWeapon { get; private set; }
    PlayerController playerController;
    float timeBetweenAttacks;
    bool attackButtonDown = false;
    bool isAttacking = false;

    protected override void Awake() 
    {
        base.Awake();

        playerController = transform.parent.GetComponent<PlayerController>();
    }

    void Start()
    {
        AttackCooldown();
    }

    void OnEnable()
    {
        playerController.OnPerformAttack += StartAttacking;
        playerController.OnCancelAttack += StopAttacking;
    }

    void OnDisable()
    {
        playerController.OnPerformAttack -= StartAttacking;
        playerController.OnCancelAttack -= StopAttacking;
    }

    void StartAttacking()
    {
        if (!attackButtonDown)
        {
            attackButtonDown = true;
            StartCoroutine(KeepAttackingCoroutine());
        }
    }

    void StopAttacking()
    {
        attackButtonDown = false;
    }

    IEnumerator KeepAttackingCoroutine()
    {
        while (attackButtonDown)
        {
            Attack();
            yield return new WaitUntil(() => !isAttacking);
        }
    }

    private void Attack()
    {
        if (!isAttacking && CurrentActiveWeapon != null)
        {
            AttackCooldown();
            (CurrentActiveWeapon as IWeapon)?.Attack();
        }
    }

    private void AttackCooldown()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            StopCoroutine("TimeBetweenAttacksRoutine");
            StartCoroutine(TimeBetweenAttacksRoutine());
        }
    }

    IEnumerator TimeBetweenAttacksRoutine()
    {
        yield return new WaitForSeconds(timeBetweenAttacks);
        isAttacking = false;
    }

    public void NewWeapon(MonoBehaviour newWeapon)
    {
        CurrentActiveWeapon = newWeapon;
        AttackCooldown();
        timeBetweenAttacks = (CurrentActiveWeapon as IWeapon).GetWeaponInfo().weaponCooldown;
    }

    public void WeaponNull()
    {
        CurrentActiveWeapon = null;
    }
}
