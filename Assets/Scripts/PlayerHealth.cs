using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] int maxHealth = 10;
    [SerializeField] float knockBackResistance = 0f;
    HealthSystem healthSystem;
    Flash flash;
    Knockback knockback;

    void Awake()
    {
        flash = GetComponent<Flash>();
        knockback = GetComponent<Knockback>();
    }

    void Start()
    {
        healthSystem = new HealthSystem(maxHealth);
    }

    public void TakeDamage(DamageSource damageSource)
    {
        healthSystem.Damage(damageSource.DamageAmount);
        StartCoroutine(flash.FlashRoutine());
        Transform transform = damageSource.transform.parent != null ?  damageSource.transform.parent.transform : damageSource.transform;
        knockback.GetKnockedBack(transform, damageSource.KnockBackThrust - knockBackResistance);
        if (damageSource.DestroyOnHit)
        {
            Destroy(transform.gameObject);
        }
        if (healthSystem.Health <= 0)
        {
            StartCoroutine(OnDeathRoutine());
        }
    }

    IEnumerator OnDeathRoutine()
    {
        yield return new WaitForSeconds(flash.RestoreDefaultMaterialTime / 2);
        Destroy(gameObject);
    }
}
