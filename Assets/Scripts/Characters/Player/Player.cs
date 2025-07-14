using System;
using System.Collections;
using UnityEngine;

public class Player : Entity
{
    [Header("Movment")]
    public float moveSpeed = 8f;
    public float jumpForce = 5f;
    public float dashSpeed = 20f;
    public Vector2 wallJumpFource;
    [Range(0f, 1f)]
    public float midairMultiplier = .7f;
    [Range(0f, 1f)]
    public float wallSlideMultiplier = .3f;
    [Space(10)]
    public float dashDuration = .25f;

    [Header("Attack")]
    public Vector2[] attackVelocity;
    public Vector2 jumpAttackVelocity;
    public float attackVelocityDuration = .1f;
    public float comboResetDuration = 1f;

    public PlayerInputSet input { get; private set; }
    public Player_IdleState idleState { get; private set; }
    public Player_MoveState moveState { get; private set; }
    public Player_JumpState jumpState { get; private set; }
    public Player_FallState fallState { get; private set; }
    public Player_WallSlideState wallSlideState { get; private set; }
    public Player_WallJumpState wallJumpState { get; private set; }
    public Player_DashState dashState { get; private set; }
    public Player_BasicAttackState basicAttackState { get; private set; }
    public Player_JumpAttackState jumpAttackState { get; private set; }
    public Player_DeadState deadState { get; private set; }

    public Vector2 movementInput { get; private set; }

    public static event Action OnPlayerDeath;

    private Coroutine queuedAttackCoroutine;

    protected override void Awake()
    {
        base.Awake();
        input = new PlayerInputSet();

        idleState = new Player_IdleState(this, stateMachine);
        moveState = new Player_MoveState(this, stateMachine);
        jumpState = new Player_JumpState(this, stateMachine);
        fallState = new Player_FallState(this, stateMachine);
        wallSlideState = new Player_WallSlideState(this, stateMachine);
        wallJumpState = new Player_WallJumpState(this, stateMachine);
        dashState = new Player_DashState(this, stateMachine);
        basicAttackState = new Player_BasicAttackState(this, stateMachine);
        jumpAttackState = new Player_JumpAttackState(this, stateMachine);
        deadState = new Player_DeadState(this, stateMachine);
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(idleState);
    }

    private void OnEnable()
    {
        input.Enable();
        input.Player.Movment.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        input.Player.Movment.canceled += ctx => movementInput = Vector2.zero;
    }

    private void OnDisable()
    {
        input.Disable();
    }

    public void EnterAttackStateWithDelay()
    {
        if (queuedAttackCoroutine != null)
            StopCoroutine(queuedAttackCoroutine);

        queuedAttackCoroutine = StartCoroutine(Coroutine_EnterAttackState());
    }

    private IEnumerator Coroutine_EnterAttackState()
    {
        yield return new WaitForEndOfFrame();
        stateMachine.ChangeState(basicAttackState);
    }

    override public void EntityDeath()
    {
        base.EntityDeath();
        OnPlayerDeath?.Invoke();
        stateMachine.ChangeState(deadState);
    }
}
