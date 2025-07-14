using UnityEngine;

public class Player_MoveState : Player_GroundedState
{
    public Player_MoveState(Player player, StateMachine stateMachine) : base(player, stateMachine, "move") { }
    public override void Update()
    {
        base.Update();

        if (player.movementInput.x == 0 || player.wallDetected)
            stateMachine.ChangeState(player.idleState);

        player.SetVelocity(player.movementInput.x * player.moveSpeed, rb.linearVelocity.y);
    }
}
