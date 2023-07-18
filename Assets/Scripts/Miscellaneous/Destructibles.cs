using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructibles : MonoBehaviour, IDamageable
{
    [SerializeField] GameObject onDestroyVFX;

    public void TakeDamage(DamageSource damageSource)
    {
        Instantiate(onDestroyVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
