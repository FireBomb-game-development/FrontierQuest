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
    [SerializeField] public float defualtDuratuion =3;
    [SerializeField] public float chillSlowMultiplier= 0.2f;


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
            float elementalDamage = stats.GetElementalDamage(out ElementalType element);

            float damage =  stats.GetPhyisicalDamage(out bool isCrit);
            
            bool succesfullHit = damegable.TakeDamage(damage,elementalDamage,element, transform);
            Debug.Log("element is:"+element);
            
            if(succesfullHit){
                if(element!= ElementalType.None) ApplyStatusEffect(target.transform, element);
                vfx.UpdateOnHitColor(element);
                vfx.CreateOnHitVFX(target.transform, isCrit);
            
            }
        }
    }

    
    public void ApplyStatusEffect(Transform target, ElementalType element)
    {
        Entity_StatusHandler statusHandler = target.GetComponent<Entity_StatusHandler>();

        if(statusHandler == null)return;

        if(element == ElementalType.Ice && statusHandler.canBeApplied(ElementalType.Ice))
        {
            statusHandler.ApplyChillEffect(defualtDuratuion, chillSlowMultiplier);
        }

    }
    protected Collider2D[] GetDetectedColiders() => Physics2D.OverlapCircleAll(targetCheck.position, targetCheckRadius, whatIsTarget);

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(targetCheck.position, targetCheckRadius);
    }

}
