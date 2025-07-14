using UnityEngine;

public class Enemy : Entity
{
    [Header("Movment")]
    public float moveSpeed = 1.4f;
    public float idleTime = 2f;

    [Header("Battle")]
    public float battleMoveSpeed = 3f;
    public float attackDistance = 2f;
    public float battleTimeDuration = 5f;
    public float minRetreatDistance = 2f;
    public Vector2 retreatVelocity;

    [Header("Player Detection")]
    [SerializeField] LayerMask playerLayer;
    [SerializeField] Transform playerCheck;
    [SerializeField] float playerCheckDistance = 10f;
    [SerializeField] float playerBackCheckDistance = 2f;

    public Enemy_IdleState idleState;
    public Enemy_MoveState moveState;
    public Enemy_AttackState attackState;
    public Enemy_BattleState battleState;
    public Enemy_DeadState deadState;

    private float defaultGroundCheckDistance;

    [Range(0, 2)]
    public float moveAnimSpeedMultiplier = 1f;

    protected override void Awake()
    {
        base.Awake();

        defaultGroundCheckDistance = groundCheckDistance;
    }

    protected override void Update()
    {
        base.Update();

        if (stateMachine.currentState == battleState)
            groundCheckDistance = 5f; // Increase ground check distance when in battle to allow falling.
        else
            groundCheckDistance = defaultGroundCheckDistance;
    }

    public override void EntityDeath()
    {
        base.EntityDeath();
        stateMachine.ChangeState(deadState);
    }

    private void HandlePlayerDeath()
    {
        stateMachine.ChangeState(idleState);
    }

    public RaycastHit2D PlayerDetecteded()
    {
        RaycastHit2D hit = Physics2D.Raycast(playerCheck.position, Vector2.right * facingDirection, playerCheckDistance, playerLayer | groundLayer);
        RaycastHit2D backHit = Physics2D.Raycast(playerCheck.position, Vector2.left * facingDirection, playerBackCheckDistance, playerLayer | groundLayer);

        if ((hit.collider == null || hit.collider.gameObject.layer != LayerMask.NameToLayer("Player")) && (backHit.collider == null || backHit.collider.gameObject.layer != LayerMask.NameToLayer("Player")))
            return default;
        else if (backHit.collider != null)
        {
            return backHit;
        }

        return hit;
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(playerCheck.position, new Vector3(playerCheck.position.x + playerCheckDistance * facingDirection, playerCheck.position.y));
        Gizmos.DrawLine(playerCheck.position, new Vector3(playerCheck.position.x + playerBackCheckDistance * -facingDirection, playerCheck.position.y));
        Gizmos.color = Color.red;
        Gizmos.DrawLine(playerCheck.position, new Vector3(playerCheck.position.x + attackDistance * facingDirection, playerCheck.position.y));
        Gizmos.color = Color.green;
        Gizmos.DrawLine(playerCheck.position, new Vector3(playerCheck.position.x + (minRetreatDistance * facingDirection), playerCheck.position.y));
    }

    private void OnEnable()
    {
        Player.OnPlayerDeath += HandlePlayerDeath;
    }

    private void OnDisable()
    {
        Player.OnPlayerDeath -= HandlePlayerDeath;
    }
}
