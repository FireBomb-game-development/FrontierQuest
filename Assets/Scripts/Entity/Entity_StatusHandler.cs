using System.Collections;
using UnityEngine;

public class Entity_StatusHandler : MonoBehaviour
{
    private ElementalType currentElementalEffect = ElementalType.None;
    private Entity entity;
    private Entity_VFX entityVfx;
    private EntityStats entityStats;
    private Entity_Health entity_Health;


    private void Awake()
    {
        entity = GetComponent<Entity>();
        entityVfx = GetComponent<Entity_VFX>();
        entityStats = GetComponent<EntityStats>();
        entity_Health = GetComponent<Entity_Health>();
    
    }

    public void ApplyBurnEffect(float duration , float totalDamage)
    {
        float fireResistence = entityStats.GetElementalResistence(ElementalType.Fire);
        float finalDamage = totalDamage *(1f-fireResistence);
        StartCoroutine(BurnEffectCo(duration,finalDamage));
    }


    private IEnumerator BurnEffectCo(float duration, float totalDamage)
    {
        currentElementalEffect = ElementalType.Fire;
        entityVfx.PlayStatusEffectColor(duration, ElementalType.Fire);
        
        int tickPerSeconed =2;
        int tickCount = Mathf.RoundToInt(tickPerSeconed * duration);

        float damagePerTick = totalDamage/ tickCount;
        float tickInterval = 1f/tickPerSeconed;

        for(int i =0; i< tickCount; i++)
        {
            entity_Health.ReduceHp(damagePerTick);
            yield return new WaitForSeconds(tickInterval);
        }
        currentElementalEffect = ElementalType.None; 
    }



    public void ApplyChillEffect(float duration, float slowMultiplier)
    {
        float iceResistence = entityStats.GetElementalResistence(ElementalType.Ice);
        float finalDuration = duration *(1-iceResistence);

        
        StartCoroutine(ChilledEffectCo(finalDuration, slowMultiplier));
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
