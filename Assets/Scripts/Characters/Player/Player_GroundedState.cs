using UnityEngine;

public class Player_GroundedState : PlayerState
{
    public Player_GroundedState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName) { }

    public override void Update()
    {
        base.Update();

        if (!player.groundDetected && rb.linearVelocityY < 0)
            stateMachine.ChangeState(player.fallState);

        if (input.Player.Jump.triggered)
            stateMachine.ChangeState(player.jumpState);

        if (input.Player.Attack.triggered)
            stateMachine.ChangeState(player.basicAttackState);
    }
}
