using UnityEngine;

public class Player_JumpAttackState : PlayerState
{

    private bool touchedGround;
    public Player_JumpAttackState(Player player, StateMachine stateMachine) : base(player, stateMachine, "jumpAttack") { }

    public override void Enter()
    {
        base.Enter();

        touchedGround = false;

        player.SetVelocity(player.jumpAttackVelocity.x * player.facingDirection, player.jumpAttackVelocity.y);
    }

    public override void Update()
    {
        base.Update();

        if (player.groundDetected && !touchedGround)
        {
            touchedGround = true;
            anim.SetTrigger("jumpAttackTrigger");
            player.SetVelocity(0, rb.linearVelocityY);
        }

        if (triggeredCalled && player.groundDetected)
            stateMachine.ChangeState(player.idleState);
    }
}
