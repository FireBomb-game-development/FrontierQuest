using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Entity_Combat : MonoBehaviour
{

    private Entity_VFX vfx;
    private EntityStats stats;
    public float damage = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Target detection")]
    [SerializeField] private Transform targetCheck;
    [SerializeField] private float targetCheckRadius =1;
    [SerializeField] private LayerMask whatIsTarget;

    [Header("Status effect details")]
    [SerializeField] private float defualtDuratuion =3;
    [SerializeField] private float chillSlowMultiplier= 0.2f;
    //[SerializeField] private float burnedDamage = 10;
    [SerializeField] private float electrifyChargeBuildUp = .3f;

    [Space]
    [SerializeField] private float fireScale =.8f;
    [SerializeField] private float lightningScale = 2.5f;


    public void Awake()
    {
        vfx = GetComponent<Entity_VFX>();
        stats = GetComponent<EntityStats>();
    }
    public void PerformeAttack()
    {
        foreach( var target in GetDetectedColiders())
        {
            IDamagable damegable = target.GetComponent<IDamagable>();
            if(damegable== null)continue;
            float elementalDamage = stats.GetElementalDamage(out ElementalType element ,.6f);

            float damage =  stats.GetPhyisicalDamage(out bool isCrit);
            Debug.Log(damage);
            
            bool succesfullHit = damegable.TakeDamage(damage,elementalDamage,element, transform);
            Debug.Log("element is:"+element);
            
            if(succesfullHit){
                if(element!= ElementalType.None) ApplyStatusEffect(target.transform, element);
                vfx.UpdateOnHitColor(element);
                vfx.CreateOnHitVFX(target.transform, isCrit);
            
            }
        }
    }

    
    public void ApplyStatusEffect(Transform target, ElementalType element, float scaleFactor =1)
    {
        Entity_StatusHandler statusHandler = target.GetComponent<Entity_StatusHandler>();

        if(statusHandler == null)return;

        if(element == ElementalType.Ice && statusHandler.CanBeApplied(ElementalType.Ice))statusHandler.ApplyChillEffect(defualtDuratuion, chillSlowMultiplier *scaleFactor);
        
        if(element ==  ElementalType.Fire && statusHandler.CanBeApplied(ElementalType.Fire))
        {
            scaleFactor = fireScale;
            float fireDamage = stats.offence.fireDamage.GetValue() * scaleFactor;
            statusHandler.ApplyBurnEffect(defualtDuratuion,fireDamage);

        }
        if(element == ElementalType.Lightning && statusHandler.CanBeApplied(ElementalType.Lightning))
        {
            scaleFactor = lightningScale;
            float lighningDamage = stats.offence.lightningDamage.GetValue()* scaleFactor;
            statusHandler.ApplyElecrifyEffect(defualtDuratuion,lighningDamage,electrifyChargeBuildUp);
        }

    }
    protected Collider2D[] GetDetectedColiders() => Physics2D.OverlapCircleAll(targetCheck.position, targetCheckRadius, whatIsTarget);

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(targetCheck.position, targetCheckRadius);
    }

}
