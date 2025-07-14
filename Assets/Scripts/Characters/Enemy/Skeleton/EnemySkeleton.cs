using UnityEngine;

public class EnemySkeleton : Enemy
{

    protected override void Awake()
    {
        base.Awake();

        idleState = new Enemy_IdleState(this, stateMachine);
        moveState = new Enemy_MoveState(this, stateMachine);
        attackState = new Enemy_AttackState(this, stateMachine);
        battleState = new Enemy_BattleState(this, stateMachine);
        deadState = new Enemy_DeadState(this, stateMachine);
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(idleState);
    }

}
