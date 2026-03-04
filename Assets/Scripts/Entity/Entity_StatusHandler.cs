using System.Collections;
using UnityEngine;

public class Entity_StatusHandler : MonoBehaviour
{
    private ElementalType currentElementalEffect = ElementalType.None;
    private Entity entity;
    private Entity_VFX entityVfx;
    private EntityStats stats;


    private void Awake()
    {
        entity = GetComponent<Entity>();
        entityVfx = GetComponent<Entity_VFX>();
        stats = GetComponent<EntityStats>();
    
    }

    public void ApplyChillEffect(float duration, float slowMultiplier)
    {
        float iceResistence = stats.GetElementalResistence(ElementalType.Ice);
        float reducedDuration = duration *(1-iceResistence);

        
        StartCoroutine(ChilledEffectCo(reducedDuration, slowMultiplier));
    }

    private IEnumerator ChilledEffectCo(float duration, float slowMultiplier)
    {
        entity.SlowDownEntity(duration, slowMultiplier);
        currentElementalEffect = ElementalType.Ice;
        entityVfx.PlayStatusEffectColor(duration,ElementalType.Ice);

        yield return new WaitForSeconds(duration);

        currentElementalEffect = ElementalType.None;
    }

    public bool canBeApplied(ElementalType elemental)
    {
        return currentElementalEffect == ElementalType.None;
    }

}
