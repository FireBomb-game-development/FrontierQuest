using System.Collections;
using UnityEngine;

public class Entity_VFX : MonoBehaviour
{

    private SpriteRenderer sr;
    private Entity entity;

    [Header("On Taking damage VFX")]
    [SerializeField] private Material onDamageMaterial;
    [SerializeField] private float onDamageVfxDuration = .2f;
    private Material originMaterial;
    private Coroutine onDamageVfxCoroitine;


    [Header("On Doing Damage VFX")]
    [SerializeField] private GameObject hitVfx;
    [SerializeField] private Color hitVfxColor = Color.white;
    [SerializeField] private GameObject critHitVfx;

    private void Awake()
    {
        entity = GetComponent<Entity>();
        sr = GetComponentInChildren<SpriteRenderer>();
        originMaterial = sr.material;
        
    }

    public void CreateOnHitVFX(Transform target, bool isCrit)
    {
        GameObject hitPrefab = isCrit ? critHitVfx : hitVfx;
        
        GameObject vfx = Instantiate(hitPrefab, target.position, Quaternion.identity);
        vfx.GetComponentInChildren<SpriteRenderer>().color = hitVfxColor;
        Debug.Log("isCrit is"+isCrit);
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
        sr.material = originMaterial;
    }

}
