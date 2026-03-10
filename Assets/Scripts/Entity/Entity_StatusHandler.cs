using System.Collections;
using UnityEngine;

public class Entity_StatusHandler : MonoBehaviour
{
    private ElementalType currentElementalEffect = ElementalType.None;
    private Entity entity;
    private Entity_VFX entityVfx;
    private EntityStats entityStats;
    private Entity_Health entity_Health;

    [Header("electrify effect details")]
    [SerializeField] private GameObject lightningStrikeVfx;
    [SerializeField] private float currentCharge;
    [SerializeField] private float maxCharge =1;
    private Coroutine electrifyCo;


    private void Awake()
    {
        entity = GetComponent<Entity>();
        entityVfx = GetComponent<Entity_VFX>();
        entityStats = GetComponent<EntityStats>();
        entity_Health = GetComponent<Entity_Health>();
    
    }

    public void ApplyElecrifyEffect( float duration, float damage, float charge)
    {
        float lightningResistence = entityStats.GetElementalResistence(ElementalType.Lightning);
        float finalCharge = charge*(1-lightningResistence);

        currentCharge += finalCharge;
        if(currentCharge >= maxCharge)
        {
            DoLightningStrike(damage);
            Debug.Log("Light damage" +damage);
            StopElectrifyEffect();
            return;
        }
        if(electrifyCo!= null)StopCoroutine(electrifyCo);
        electrifyCo = StartCoroutine(ElectrifyEffectCo(duration));
    }

    private void StopElectrifyEffect()
    {
        currentCharge =0;
        currentElementalEffect = ElementalType.None;
        entityVfx.StopAllVfx();
    }
    private void DoLightningStrike(float damage)
    {
        Instantiate(lightningStrikeVfx,transform.position,Quaternion.identity);
        entity_Health.ReduceHp(damage);
    }

    private IEnumerator ElectrifyEffectCo( float duration)
    {
        currentElementalEffect = ElementalType.Lightning;
        entityVfx.PlayStatusEffectColor(duration,ElementalType.Lightning);
        yield return new WaitForSeconds(duration);
        StopElectrifyEffect();
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

        
        StartCoroutine(ChillEffectCo(finalDuration, slowMultiplier));
    }

    private IEnumerator ChillEffectCo(float duration, float slowMultiplier)
    {
        entity.SlowDownEntity(duration, slowMultiplier);
        currentElementalEffect = ElementalType.Ice;
        entityVfx.PlayStatusEffectColor(duration,ElementalType.Ice);

        yield return new WaitForSeconds(duration);

        currentElementalEffect = ElementalType.None;
    }

    public bool CanBeApplied(ElementalType element)
    {
        if(element ==  ElementalType.Lightning && currentElementalEffect == ElementalType.Lightning) return true;
        return currentElementalEffect == ElementalType.None;
    }

}
