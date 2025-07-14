using UnityEngine;

public class Player_AiredState : PlayerState
{
    public Player_AiredState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName) { }

    public override void Update()
    {
        base.Update();

        if (player.movementInput.x != 0)
            player.SetVelocity(player.movementInput.x * player.moveSpeed * player.midairMultiplier, player.rb.linearVelocityY);

        if (input.Player.Attack.triggered)
            stateMachine.ChangeState(player.jumpAttackState);
    }
}
