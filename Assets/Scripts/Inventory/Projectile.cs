using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float moveSpeed = 22f;
    [SerializeField] float range = 7f;
    DamageSource myDamageSource;
    Vector3 startPosition;

    void Awake()
    {
        myDamageSource = GetComponent<DamageSource>();
    }

    void Start()
    {
        startPosition = transform.position;
    }

    void FixedUpdate()
    {
        MoveProjectile();
        DestroyAfterRange();
    }

    void DestroyAfterRange()
    {
        if (Vector3.Distance(transform.position, startPosition) > range)
        {
            Destroy(transform.parent != null ? transform.parent.gameObject : gameObject);
        }
    }

    void MoveProjectile()
    {
        transform.Translate(Vector3.right * Time.fixedDeltaTime * moveSpeed);
    }
}
