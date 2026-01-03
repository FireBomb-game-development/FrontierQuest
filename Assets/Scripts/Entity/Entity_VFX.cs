using System.Collections;
using UnityEngine;

public class Entity_VFX : MonoBehaviour
{

    private SpriteRenderer sr;

    [Header("On damage VFX")]
    [SerializeField] private Material onDamageMaterial;
    [SerializeField] private float onDamageVfxDuration = .2f;
    private Material originMaterial;
    private Coroutine onDamageVfxCoroitine;


    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        originMaterial = sr.material;
    }


    public void PlayOnDamageVfx()
    {
        if (onDamageVfxCoroitine != null) StopCoroutine(onDamageVfxCoroitine);
        onDamageVfxCoroitine = StartCoroutine(onDamageVfxCo());
    }

    private IEnumerator onDamageVfxCo()
    {

        sr.material = onDamageMaterial;
        yield return new WaitForSeconds(onDamageVfxDuration);
        sr.material = originMaterial;
    }

}
