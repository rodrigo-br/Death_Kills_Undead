using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    public delegate void PerformAttack();
    public PerformAttack OnPerformAttack;
    public delegate void CancelAttack();
    public CancelAttack OnCancelAttack;
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D myRigidBody;
    private Vector2 movement;
    private PlayerControls playerControls;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRenderer;
    private Dash dash;
    public bool FacingLeft { get; private set; }
    Knockback knockback;

    protected override void Awake() 
    {
        base.Awake();

        playerControls = new PlayerControls();
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        dash = GetComponent<Dash>();
        knockback = GetComponent<Knockback>();
    }

    private void Update() 
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        if (!knockback.gettingKnockedBack)
        {
            Move();
            AdjustPlayerFacingDirection();
        }
    }

    private void Move()
    {
        myRigidBody.MovePosition(myRigidBody.position + movement * (moveSpeed * dash.defaultSpeed * Time.fixedDeltaTime));
    }

    private void OnEnable() 
    {
        playerControls.Enable();
        playerControls.Player.Attack.started += _ => OnPerformAttack?.Invoke();
        playerControls.Player.Attack.canceled += _ => OnCancelAttack?.Invoke();
        playerControls.Player.Dash.performed += _ => dash.PerformDash();
    }

    private void OnDisable() 
    {
        playerControls?.Disable();
    }

    private void PlayerInput()
    {
        movement = playerControls.Player.Move.ReadValue<Vector2>();
        myAnimator.SetFloat(StringsDefinitions.MOVE_X, Mathf.Abs(movement.x));
        myAnimator.SetFloat(StringsDefinitions.MOVE_Y, Mathf.Abs(movement.y));
    }

    private void AdjustPlayerFacingDirection()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        FacingLeft = mySpriteRenderer.flipX = (mousePos.x < playerScreenPoint.x);
    }
}
