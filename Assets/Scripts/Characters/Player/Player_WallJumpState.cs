using UnityEngine;

public class Player_WallJumpState : PlayerState
{
    public Player_WallJumpState(Player player, StateMachine stateMachine) : base(player, stateMachine, "jumpFall") { }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocity(player.wallJumpFource.x * -player.facingDirection, player.wallJumpFource.y);
    }

    public override void Update()
    {
        base.Update();

        if (rb.linearVelocityY < 0)
            stateMachine.ChangeState(player.fallState);

        if (player.wallDetected)
            stateMachine.ChangeState(player.wallSlideState);
    }
}
