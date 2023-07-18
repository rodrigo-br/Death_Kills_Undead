using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    [SerializeField] float knockbackTime = 0.2f;
    private Rigidbody2D myRigidBody;
    public bool gettingKnockedBack { get; private set; }

    private void Awake()
    {
        myRigidBody = GetComponent<Rigidbody2D>();    
    }

    public void GetKnockedBack(Transform damageSource, float knockBackThrust)
    {
        if (knockBackThrust <= 0 || gettingKnockedBack) { return ; }

        gettingKnockedBack = true;
        Vector2 force = (transform.position - damageSource.position).normalized *
                        knockBackThrust *
                        myRigidBody.mass;
        myRigidBody.AddForce(force, ForceMode2D.Impulse);
        StartCoroutine(KnockRoutine());
    }

    private IEnumerator KnockRoutine()
    {
        yield return new WaitForSeconds(knockbackTime);
        myRigidBody.velocity = Vector2.zero;
        gettingKnockedBack = false;
    }
}
