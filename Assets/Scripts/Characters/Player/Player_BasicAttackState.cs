using UnityEngine;

public class Player_BasicAttackState : PlayerState
{
    private const int ComboStartIndex = 0;
    private const int BasicAttackAnimationsCount = 3;
    private float attackVelocityTimer;
    private int comboIndex = ComboStartIndex;
    private float lastTimeAttacked;
    private bool comboAttackQueued = false;
    private int attackDirection;

    public Player_BasicAttackState(Player player, StateMachine stateMachine) : base(player, stateMachine, "basicAttack")
    {
        if (BasicAttackAnimationsCount != player.attackVelocity.Length)
        {
            throw new System.Exception($"Player attack velocity array length ({player.attackVelocity.Length}) does not match the expected number of basic attack animations ({BasicAttackAnimationsCount}). Please ensure they are consistent.");
        }
    }

    public override void Enter()
    {
        base.Enter();

        comboAttackQueued = false;

        ResetComboIndexIfNeeded();

        attackDirection = player.movementInput.x != 0 ? ((int)player.movementInput.x) : player.facingDirection;

        anim.SetInteger("basicAttackIndex", comboIndex);

        ApplyAttackVelocity();
    }

    public override void Update()
    {
        base.Update();

        HandleAttackVelocity();

        if (input.Player.Attack.triggered)
            QueueNextAttack();

        if (triggeredCalled)
            HandleStateExit();
    }

    public override void Exit()
    {
        base.Exit();

        comboIndex++;

        lastTimeAttacked = Time.time;
    }

    private void HandleStateExit()
    {
        if (comboAttackQueued)
        {
            anim.SetBool(animBoolName, false);
            player.EnterAttackStateWithDelay();
        }
        else
            stateMachine.ChangeState(player.idleState);
    }

    private void HandleAttackVelocity()
    {
        attackVelocityTimer -= Time.deltaTime;

        if (attackVelocityTimer < 0)
            player.SetVelocity(0, rb.linearVelocityY);
    }

    private void ApplyAttackVelocity()
    {
        Vector2 attackVelocity = player.attackVelocity[comboIndex];
        attackVelocityTimer = player.attackVelocityDuration;

        player.SetVelocity(attackVelocity.x * attackDirection, attackVelocity.y);
    }

    private void QueueNextAttack()
    {
        int lastIndex = BasicAttackAnimationsCount - 1;
        if (comboIndex < lastIndex)
            comboAttackQueued = true;
    }

    private void ResetComboIndexIfNeeded()
    {
        int lastIndex = BasicAttackAnimationsCount - 1;
        if (comboIndex > lastIndex || Time.time > lastTimeAttacked + player.comboResetDuration)
            comboIndex = ComboStartIndex;
    }
}
