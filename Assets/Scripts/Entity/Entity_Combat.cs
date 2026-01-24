using UnityEngine;

public class Entity_Combat : MonoBehaviour
{

    private Entity_VFX vfx;
    public float damage = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Target detection")]
    [SerializeField] private Transform targetCheck;
    [SerializeField] private float targetCheckRadius =1;
    [SerializeField] private LayerMask whatIsTarget;


    public void Awake()
    {
        vfx = GetComponent<Entity_VFX>();
    }
    public void PerformeAttack()
    {
        foreach( var target in GetDetectedColiders())
        {
            IDamagable damagable = target.GetComponent<IDamagable>();
            if(damagable== null)continue;
            bool succesfullHit = damagable.TakeDamage(damage, transform);
            if(succesfullHit)vfx.CreateOnHitVFX(target.transform);
            
        }
    }

    protected Collider2D[] GetDetectedColiders() => Physics2D.OverlapCircleAll(targetCheck.position, targetCheckRadius, whatIsTarget);

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(targetCheck.position, targetCheckRadius);
    }

}
