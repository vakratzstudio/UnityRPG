using UnityEngine;

public class Chest : MonoBehaviour, IDamageable
{
    private Rigidbody2D rb;
    private Animator anim;
    private Entity_VFX entityVFX;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        entityVFX = GetComponentInChildren<Entity_VFX>();
    }

    public void TakeDamage(float damage)
    {
        anim.SetBool("open", true);
        rb.linearVelocity = new Vector2(0, 3f);
        rb.angularVelocity = Random.Range(-200f, 200f);
        entityVFX?.PlayOnDamageVfx();
    }
}
