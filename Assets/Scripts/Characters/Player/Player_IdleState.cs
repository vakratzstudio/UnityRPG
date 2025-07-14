using UnityEngine;

public class Player_IdleState : Player_GroundedState
{
    public Player_IdleState(Player player, StateMachine stateMachine) : base(player, stateMachine, "idle") { }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocity(0, rb.linearVelocityY);
    }

    public override void Update()
    {
        base.Update();

        if (player.movementInput.x == player.facingDirection && player.wallDetected)
            return;

        if (player.movementInput.x != 0)
            stateMachine.ChangeState(player.moveState);
    }
}

