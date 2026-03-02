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
            float elementalDamage = stats.GetElementalDamage();

            float damage =  stats.GetPhyisicalDamage(out bool isCrit);
            
            bool succesfullHit = damegable.TakeDamage(damage,elementalDamage, transform);
            if(succesfullHit){
                vfx.CreateOnHitVFX(target.transform, isCrit);
            
            }
        }
    }

    protected Collider2D[] GetDetectedColiders() => Physics2D.OverlapCircleAll(targetCheck.position, targetCheckRadius, whatIsTarget);

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(targetCheck.position, targetCheckRadius);
    }

}
