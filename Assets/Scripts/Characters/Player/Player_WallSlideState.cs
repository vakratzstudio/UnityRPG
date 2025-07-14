using UnityEngine;

public class Player_WallSlideState : PlayerState
{
    public Player_WallSlideState(Player player, StateMachine stateMachine) : base(player, stateMachine, "wallSlide") { }


    public override void Update()
    {
        base.Update();

        HandleWallSlide();

        if (input.Player.Jump.triggered)
            stateMachine.ChangeState(player.wallJumpState);

        if (!player.wallDetected)
            stateMachine.ChangeState(player.fallState);

        if (player.groundDetected)
        {
            stateMachine.ChangeState(player.idleState);

            if (player.facingDirection != player.movementInput.x)
                player.Flip();
        }
    }

    private void HandleWallSlide()
    {
        if (player.movementInput.y < 0)
            player.SetVelocity(player.movementInput.x, rb.linearVelocityY);
        else
            player.SetVelocity(player.movementInput.x, rb.linearVelocityY * player.wallSlideMultiplier);


    }
}
