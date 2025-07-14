using UnityEngine;

public class Player_JumpState : Player_AiredState
{
    public Player_JumpState(Player player, StateMachine stateMachine) : base(player, stateMachine, "jumpFall") { }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocity(rb.linearVelocityX, player.jumpForce);
    }

    public override void Update()
    {
        base.Update();

        if (rb.linearVelocityY < 0 && stateMachine.currentState != player.jumpAttackState)
            stateMachine.ChangeState(player.fallState);            
    }
}
