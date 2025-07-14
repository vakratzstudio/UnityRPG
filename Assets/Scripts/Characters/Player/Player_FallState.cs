using UnityEngine;

public class Player_FallState : Player_AiredState
{
    public Player_FallState(Player player, StateMachine stateMachine) : base(player, stateMachine, "jumpFall") { }

    public override void Update()
    {
        base.Update();

        if (player.groundDetected)
            stateMachine.ChangeState(player.idleState);

        if (player.wallDetected)
            stateMachine.ChangeState(player.wallSlideState);
    }
}
