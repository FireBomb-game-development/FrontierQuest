using UnityEngine;

public class Entity_Combat : MonoBehaviour
{

    public float damage = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Target detection")]
    [SerializeField] private Transform targetCheck;
    [SerializeField] private float targetCheckRadius =1;
    [SerializeField] private LayerMask whatIsTarget;


    public void PerformeAttack()
    {
        foreach( var target in GetDetectedColiders())
        {
            IDamagable damagable = target.GetComponent<IDamagable>();
            damagable?.TakeDamage(damage,transform);
        }
    }

    protected Collider2D[] GetDetectedColiders() => Physics2D.OverlapCircleAll(targetCheck.position, targetCheckRadius, whatIsTarget);

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(targetCheck.position, targetCheckRadius);
    }

}
