using UnityEngine;

public class Enemy_MoveState : Enemy_GroundedState
{
    public Enemy_MoveState(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine, "move") { }

    public override void Enter()
    {
        base.Enter();

        if (!enemy.groundDetected || enemy.wallDetected)
            enemy.Flip();
    }

    public override void Update()
    {
        base.Update();

        enemy.SetVelocity(enemy.moveSpeed * enemy.facingDirection, rb.linearVelocityY);

        if (!enemy.groundDetected || enemy.wallDetected)
            stateMachine.ChangeState(enemy.idleState);
    }
}
