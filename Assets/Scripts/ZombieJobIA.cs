using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieJobIA : MonoBehaviour
{
    private Rigidbody2D myRigidBody;
    private Vector2 moveDirection;
    Knockback knockback;
    public void SetMoveDirection(Vector2 value) => moveDirection = value;
    public void SetLocalScale(Vector2 value) => transform.localScale = value;
    public Vector2 MoveDirection => moveDirection;

    private void Awake() 
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        knockback = GetComponent<Knockback>();
    }

    public bool CanMove() => !knockback.gettingKnockedBack;

    public void Move(Vector2 pos) => myRigidBody.MovePosition(pos);
}
