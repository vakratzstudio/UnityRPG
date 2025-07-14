using UnityEngine;

public class Entity_Health : MonoBehaviour
{
    [SerializeField] protected bool isDead;

    public float maxHp = 100f;
    private Entity entity;

    protected virtual void Awake()
    {
        entity = GetComponent<Entity>();
    }

    public virtual void TakeDamage(float damage)
    {
        if (isDead) return;

        maxHp -= damage;

        if (maxHp <= 0)
        {
            isDead = true;
            entity.EntityDeath();
        }
    }
}
