using UnityEngine;
using System.Collections;

public class Entity_VFX : MonoBehaviour
{
    [Header("On Damage VFX")]
    [SerializeField] private Material onDamageMaterial;
    [SerializeField] private float onDamageVfxDuration = 0.15f;

    private SpriteRenderer spriteRenderer;
    private Material originalMaterial;
    private Coroutine onDamageVfxCoroutine;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        originalMaterial = spriteRenderer.material;
    }

    public void PlayOnDamageVfx() {
        if (onDamageVfxCoroutine != null)
            StopCoroutine(onDamageVfxCoroutine);
        
        onDamageVfxCoroutine = StartCoroutine(OnDamageVfxCoroutine());
    }

    private IEnumerator OnDamageVfxCoroutine() {
        spriteRenderer.material = onDamageMaterial;
        yield return new WaitForSeconds(onDamageVfxDuration);
        spriteRenderer.material = originalMaterial;
    }
}
