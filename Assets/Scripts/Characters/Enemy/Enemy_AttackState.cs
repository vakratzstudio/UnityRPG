using UnityEngine;

public class Enemy_AttackState : EnemyState
{
    public Enemy_AttackState(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine, "attack") { }

    public override void Update()
    {
        base.Update();

        if (triggeredCalled)
            stateMachine.ChangeState(enemy.battleState);
    }
}
