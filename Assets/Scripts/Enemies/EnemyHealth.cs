using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] int maxHealth = 5;
    [SerializeField] float knockBackResistance = 0f;
    [SerializeField] GameObject deathVFXPrefab;
    HealthSystem healthSystem;
    Knockback knockback;
    Flash flash;

    void Awake()
    {
        knockback = GetComponent<Knockback>();
        flash = GetComponent<Flash>();
    }

    void Start()
    {
        healthSystem = new HealthSystem(maxHealth);
    }

    public void TakeDamage(DamageSource damageSource)
    {
        healthSystem.Damage(damageSource.DamageAmount);
        StartCoroutine(flash.FlashRoutine());
        knockback.GetKnockedBack(damageSource.transform.parent.transform, damageSource.KnockBackThrust - knockBackResistance);
        if (damageSource.DestroyOnHit)
        {
            Destroy(damageSource.transform.parent.gameObject);
        }
        if (healthSystem.Health <= 0)
        {
            StartCoroutine(OnDeathRoutine());
        }
    }

    IEnumerator OnDeathRoutine()
    {
        yield return new WaitForSeconds(flash.RestoreDefaultMaterialTime / 2);
        Instantiate(deathVFXPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
