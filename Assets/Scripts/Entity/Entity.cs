using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour
{
    [Header("Collision Detection")]
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private Transform headWallCheck;
    [SerializeField] private Transform legsWallCheck;
    [SerializeField] private Transform groundCheck;
    [SerializeField] protected LayerMask groundLayer;
    [SerializeField] protected float groundCheckDistance;

    public bool groundDetected { get; private set; }
    public bool wallDetected { get; private set; }
    public Animator anim { get; private set; }
    public int facingDirection { get; private set; } = 1;
    public Rigidbody2D rb { get; private set; }

    protected StateMachine stateMachine;
    private bool isFacingRight = true;
    private bool isKnockedBack = false;
    private Coroutine knockbackCoroutine;

    protected virtual void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        stateMachine = new StateMachine();
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        stateMachine.UpdateActiveState();
        HandleCollitionDetection();
    }

    public void SetVelocity(float x, float y)
    {
        if (isKnockedBack) return;

        rb.linearVelocity = new Vector2(x, y);
        HandleFlip(x);
    }

    public void HandleFlip(float xInput)
    {
        if (xInput > 0f && !isFacingRight)
            Flip();
        else if (xInput < 0f && isFacingRight)
            Flip();
    }

    public void Flip()
    {
        transform.Rotate(0f, 180f, 0f);
        isFacingRight = !isFacingRight;
        facingDirection *= -1;
    }

    public void CurrentStateAnimationTrigger()
    {
        stateMachine.currentState.AnimationTrigger();
    }

    public void Knockback(Vector2 knockbackDirection, float knockbackDuration)
    {
        if (knockbackCoroutine != null)
            StopCoroutine(knockbackCoroutine);

        knockbackCoroutine = StartCoroutine(KnockbackCoroutine(knockbackDirection, knockbackDuration));
    }

    private IEnumerator KnockbackCoroutine(Vector2 knockbackDirection, float knockbackDuration)
    {
        isKnockedBack = true;
        rb.linearVelocity = knockbackDirection;
        yield return new WaitForSeconds(knockbackDuration);
        rb.linearVelocity = Vector2.zero;
        isKnockedBack = false;
    }

    private void HandleCollitionDetection()
    {
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);

        if (legsWallCheck != null)
        {
            wallDetected = Physics2D.Raycast(headWallCheck.position, Vector2.right * facingDirection, wallCheckDistance, groundLayer)
                        && Physics2D.Raycast(legsWallCheck.position, Vector2.right * facingDirection, wallCheckDistance, groundLayer);
        }
        else
            wallDetected = Physics2D.Raycast(headWallCheck.position, Vector2.right * facingDirection, wallCheckDistance, groundLayer);
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundCheckDistance);
        Gizmos.DrawLine(headWallCheck.position, headWallCheck.position + new Vector3(wallCheckDistance * facingDirection, 0));
        if (legsWallCheck != null)
            Gizmos.DrawLine(legsWallCheck.position, legsWallCheck.position + new Vector3(wallCheckDistance * facingDirection, 0));
    }

    public virtual void EntityDeath() { }
}
