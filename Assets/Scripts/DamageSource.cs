using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSource : MonoBehaviour
{
    [SerializeField] int damageAmount = 1;
    [SerializeField] float knockBackThrust = 2f;
    [SerializeField] GameObject particleOnHitPrefab;
    [SerializeField] bool destroyOnHit = false;
    [SerializeField] float damageSourceColliderCooldown = 0f;
    public bool DestroyOnHit => destroyOnHit;
    public int DamageAmount => damageAmount;
    public float KnockBackThrust => knockBackThrust;

    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
        Indestructible indestructible = other.gameObject.GetComponent<Indestructible>();

        if (damageable != null)
        {
            damageable?.TakeDamage(this);
            if (particleOnHitPrefab != null)
            {
                Instantiate(particleOnHitPrefab, transform.position, transform.rotation);
            }
        }
        else if (!other.isTrigger && indestructible != null)
        {
            if (particleOnHitPrefab != null)
            {
                Instantiate(particleOnHitPrefab, transform.position, transform.rotation);
            }
            if (destroyOnHit)
            {
                Destroy(transform.parent.gameObject);
            }
        }
        if (damageSourceColliderCooldown > 0)
        {
            StartCoroutine(ColliderCooldownRoutine());
        }
    }

    IEnumerator ColliderCooldownRoutine()
    {
        Collider2D myCollider = GetComponent<Collider2D>();
        myCollider.enabled = false;
        yield return new WaitForSeconds(damageSourceColliderCooldown);
        myCollider.enabled = true;
    }
}
