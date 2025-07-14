using UnityEngine;

public class Enemy_DeadState : EnemyState
{
    public Enemy_DeadState(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine, "") { }

    public override void Enter()
    {
        anim.enabled = false;

        rb.gravityScale = 12;
        rb.linearVelocity = new Vector2(rb.linearVelocityX, 15);
        
        enemy.GetComponent<Collider2D>().enabled = false;

    }
}
