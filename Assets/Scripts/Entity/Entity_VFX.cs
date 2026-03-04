using System.Collections;
using UnityEngine;

public class Entity_VFX : MonoBehaviour
{

    private SpriteRenderer sr;
    private Entity entity;

    [Header("On Taking damage VFX")]
    [SerializeField] private Material onDamageMaterial;
    [SerializeField] private float onDamageVfxDuration = .2f;
    private Material originalMaterial;
    private Coroutine onDamageVfxCoroitine;


    [Header("On Doing Damage VFX")]
    [SerializeField] private GameObject hitVfx;
    [SerializeField] private Color hitVfxColor = Color.white;
    [SerializeField] private GameObject critHitVfx;

    [Header("Elements Colors")]
    [SerializeField] private Color chillVfx =  Color.cyan;
    private Color originalHitVfxColor;

    private void Awake()
    {
        entity = GetComponent<Entity>();
        sr = GetComponentInChildren<SpriteRenderer>();
        originalMaterial = sr.material;
        originalHitVfxColor = hitVfxColor;
        
    }
    public void PlayStatusEffectColor( float duration, ElementalType element)
    {
        if( element == ElementalType.Ice)
        {
            StartCoroutine(PlayStatusEffectColorCo(duration,chillVfx));
        }
    }
    private IEnumerator PlayStatusEffectColorCo(float duration, Color effectColor)
    {
        float blinkInterval = .25f;
        float timer = 0 ;
        
        Color lightColor = effectColor *1.2f;
        Color darkColor = effectColor * .8f;

        bool toggle = false;

        while(timer< duration)
        {
            sr.color = toggle? darkColor :lightColor;
            toggle = !toggle;

            yield return new WaitForSeconds(blinkInterval);
            timer+= blinkInterval;
        }
        sr.color =Color.white;
    }
    public void UpdateOnHitColor(ElementalType element)
    {
        switch (element)
        {
            case ElementalType.Ice:
                hitVfxColor = chillVfx;
                break;

            case ElementalType.None:
                hitVfxColor = originalHitVfxColor;
                break;
        }
    }
    public void CreateOnHitVFX(Transform target, bool isCrit)
    {
        GameObject hitPrefab = isCrit ? critHitVfx : hitVfx;
        
        GameObject vfx = Instantiate(hitPrefab, target.position, Quaternion.identity);
        vfx.GetComponentInChildren<SpriteRenderer>().color = hitVfxColor;
        if(isCrit )Debug.Log("critical Hit");
        if(entity.facingDiraction == -1 && isCrit) vfx.transform.Rotate(0,180,0);
        
            
        
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
        sr.material = originalMaterial;
    }

}
