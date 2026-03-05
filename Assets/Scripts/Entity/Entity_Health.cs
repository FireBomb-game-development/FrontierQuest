using System;
using UnityEngine;
using UnityEngine.UI;

public class Entity_Health : MonoBehaviour , IDamagable
{
    private Slider healthBar;
    private Entity_VFX entityVfx;
    private Entity entity;
    private EntityStats stats;
    [SerializeField]protected float currentHP;
    [SerializeField] protected bool isDead;

    [Header("On Damage KnockBack")]
    [SerializeField] private Vector2 knockBackPower = new Vector2(1.5f, 2.5f);
    [SerializeField] private Vector2 heavyKnockbackPower = new Vector2(7, 7);
    [SerializeField] private float KnockBackDuration = .2f;
    [SerializeField] private float HeavyKnockbackDuration = .5f;
    [Header("On Heavy damage")]
    [SerializeField] private float heavyDamageThreshold = .3f;


    public void Awake()
    {
        entityVfx = GetComponent<Entity_VFX>();
        entity = GetComponent<Entity>();
        healthBar = GetComponentInChildren<Slider>();
        stats = GetComponent<EntityStats>();
        currentHP = stats.GetMaxHealth();
        UpdateHealthBar();
    }
    public virtual bool TakeDamage(float damage,float elementalDamage,ElementalType element,
    Transform damageDealer)
    {
        if (isDead) return false;
        if (AttackEvaded())
        {
            Debug.Log($"{gameObject.name} evaded the attack");
            return false;
        }
        EntityStats attackerStats = damageDealer.GetComponent<EntityStats>();
        float aramorReduction = attackerStats != null ? attackerStats.GetArmorReduction() : 0;
        float mitigation = stats.GetArmorMitigation(aramorReduction);
        float physicalDamageTaken = damage * (1 - mitigation);

        float ElementalResistence = stats.GetElementalResistence(element);
        float elementalDamageTaken = elementalDamage * (1 - ElementalResistence);

        TakeKnockback(damageDealer, physicalDamageTaken);
        ReduceHp(physicalDamageTaken+ elementalDamageTaken);



        return true;
    }

    private void TakeKnockback(Transform damageDealer, float finalDamage)
    {
        Vector2 knockback = CalculateKnockback(finalDamage, damageDealer);
        float duration = CalculateDuration(finalDamage);
        entity?.ReciveKnockBack(knockback, duration);
    }

    public bool AttackEvaded() => UnityEngine.Random.Range(0, 100) < stats.getEvasion();

    public void ReduceHp(float damage)
    {
        entityVfx?.PlayOnDamageVfx();
        currentHP -= damage;
        UpdateHealthBar();
        if (currentHP <= 0) Die();
    }

    private void Die()
    {
        isDead = true;
        Debug.Log("entity died");
        entity.EntityDeath();
    }
    private void UpdateHealthBar()
    {
        if (healthBar == null) return;
        healthBar.value = currentHP / stats.GetMaxHealth();
    } 
    private Vector2 CalculateKnockback(float damage,Transform damageDealer)
    {
        int diretion = transform.position.x > damageDealer.position.x ? 1 : -1;
        Vector2 knockback = IsHeavyDamage(damage)? heavyKnockbackPower:knockBackPower;
        knockback.x *= diretion;
        return knockback;
    }
    private float CalculateDuration(float damage) => IsHeavyDamage(damage) ? HeavyKnockbackDuration : KnockBackDuration;
    private bool IsHeavyDamage(float damage) => damage / stats.GetMaxHealth() > heavyDamageThreshold;
}
