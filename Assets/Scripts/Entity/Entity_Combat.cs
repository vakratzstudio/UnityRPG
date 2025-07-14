using UnityEngine;

public class Entity_Combat : MonoBehaviour
{
    [Header("Target Detection")]
    [SerializeField] private Transform targetDetectionPoint;
    [SerializeField] private float targetDetectionRadius = 1f;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private int demageInflicted = 1;

    public void PerformAttack()
    {
        foreach (var target in GetDetectedTargets())
        {
            IDamageable targetHealth = target.GetComponent<IDamageable>();
            targetHealth?.TakeDamage(demageInflicted);

            Entity_VFX entityVFX = target.GetComponent<Entity_VFX>();
            entityVFX?.PlayOnDamageVfx();

            Entity_Knockback entityKnockback = target.GetComponent<Entity_Knockback>();
            Entity attacker = GetComponent<Entity>();
            entityKnockback?.PerformKnockback(attacker.facingDirection, demageInflicted);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(targetDetectionPoint.position, targetDetectionRadius);
    }

    private Collider2D[] GetDetectedTargets() => Physics2D.OverlapCircleAll(targetDetectionPoint.position, targetDetectionRadius, targetLayer);
}
