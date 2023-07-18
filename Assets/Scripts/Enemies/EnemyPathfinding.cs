using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    private Rigidbody2D myRigidBody;
    private Vector2 moveDirection;
    Knockback knockback;
    bool flipped = false;
    public Vector2 MoveDirection => moveDirection;
    public void SetMoveDirection(Vector2 value) => moveDirection = value;

    private void Awake() 
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        knockback = GetComponent<Knockback>();
    }

    private void FixedUpdate() 
    {
        if (!knockback.gettingKnockedBack)
        {
            myRigidBody.MovePosition((Vector2)transform.position + moveDirection * (moveSpeed * Time.fixedDeltaTime));
            if (moveDirection.x < 0 && !flipped || moveDirection.x > 0 && flipped)
            {
                flipped = transform.localScale.x > 0;
                transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            }
        }
    }
}

