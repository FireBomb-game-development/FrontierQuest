using System;
using UnityEngine;

public class Entity_Health : MonoBehaviour , IDamagable
{
    private Entity_VFX entityVfx;
    private Entity entity;
    [SerializeField] protected float currentHP;
    [SerializeField] protected float maxHp = 100;
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
        currentHP = maxHp;
    }
    public virtual void TakeDamage(float damage, Transform damageDealer)
    {
        if (isDead) return;

        Vector2 knockback = CalculateKnockback(damage, damageDealer);
        float duration = CalculateDuration(damage);
        entity?.ReciveKnockBack(knockback, duration);
        entityVfx?.PlayOnDamageVfx();
        ReduceHp(damage);
    }

    protected void ReduceHp(float damage)
    {
        currentHP -= damage;
        if (currentHP <= 0) Die();
    }

    private void Die()
    {
        isDead = true;
        Debug.Log("entity died");
        entity.EntityDeath();
    }

    private Vector2 CalculateKnockback(float damage,Transform damageDealer)
    {
        int diretion = transform.position.x > damageDealer.position.x ? 1 : -1;
        Vector2 knockback = IsHeavyDamage(damage)? heavyKnockbackPower:knockBackPower;
        knockback.x *= diretion;
        return knockback;
    }
    private float CalculateDuration(float damage) => IsHeavyDamage(damage) ? HeavyKnockbackDuration : KnockBackDuration;
    private bool IsHeavyDamage(float damage) => damage / maxHp > heavyDamageThreshold;
}
