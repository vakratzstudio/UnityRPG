using System;
using UnityEngine;

public class Enemy_BattleState : EnemyState
{
    private Transform player;
    private float lastTimeWasInBattle;

    public Enemy_BattleState(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine, "battle") { }

    public override void Enter()
    {
        base.Enter();

        if (player == null)
            player = enemy.PlayerDetecteded().transform;

        if (ShouldRetreat())
        {
            rb.linearVelocity = new Vector2(enemy.retreatVelocity.x * -DirectionToPlayer(), enemy.retreatVelocity.y);
            enemy.HandleFlip(DirectionToPlayer());
        }
    }

    public override void Update()
    {
        base.Update();

        if (enemy.PlayerDetecteded())
            UpdateBattleTimer();

        if (IsPlayerInRange() && enemy.PlayerDetecteded())
            stateMachine.ChangeState(enemy.attackState);
        else
            enemy.SetVelocity(enemy.battleMoveSpeed * DirectionToPlayer(), rb.linearVelocityY);

        if (BattleIsOver() || !enemy.groundDetected || enemy.wallDetected)
            stateMachine.ChangeState(enemy.idleState);
    }

    private void UpdateBattleTimer() => lastTimeWasInBattle = Time.time;

    private bool BattleIsOver() => Time.time > enemy.battleTimeDuration + lastTimeWasInBattle;

    private bool ShouldRetreat() => DistanceToPlayer() < enemy.minRetreatDistance;

    private int DirectionToPlayer()
    {
        if (player == null) return 0;

        float distanceOffset = 0.5f;
        if (Mathf.Abs(player.position.x - enemy.transform.position.x) < distanceOffset)
            return 0;

        return player.position.x > enemy.transform.position.x ? 1 : -1;
    }

    private bool IsPlayerInRange() => DistanceToPlayer() < enemy.attackDistance;

    private float DistanceToPlayer()
    {
        if (player == null) return float.MaxValue;

        return Mathf.Abs(player.position.x - enemy.transform.position.x);
    }
}
