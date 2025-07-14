using UnityEngine;

public class Entity_Knockback : MonoBehaviour
{
    [Header("Knockback Settings")]
    [SerializeField] private Vector2 knockbackDirection = new Vector2(1.2f, 2.5f);
    [SerializeField] private float knockbackDuration = .2f;

    [Header("Heavy Knockback Settings")]
    [Range(0, 1)]
    [SerializeField] private float havyKnockbackThreshold = .3f; // percentage of health to trigger heavy knockback
    [SerializeField] private Vector2 havyKnockbackDirection = new Vector2(7f, 27f);
    [SerializeField] private float havyKnockbackDuration = .5f;

    private Entity entity;
    private Entity_Health entityHealth;

    private void Awake()
    {
        entity = GetComponent<Entity>();
        entityHealth = GetComponent<Entity_Health>();
    }

    public void PerformKnockback(int attackDirection, float damage)
    {
        float healthPercentageTaken = damage / entityHealth.maxHp;
        bool knockbackIsHeavy = healthPercentageTaken >= havyKnockbackThreshold;

        Vector2 direction = knockbackIsHeavy
            ? new Vector2(havyKnockbackDirection.x * attackDirection, havyKnockbackDirection.y)
            : new Vector2(knockbackDirection.x * attackDirection, knockbackDirection.y);

        float duration = knockbackIsHeavy ? havyKnockbackDuration : knockbackDuration;

        entity.Knockback(direction, duration);
    }
}
