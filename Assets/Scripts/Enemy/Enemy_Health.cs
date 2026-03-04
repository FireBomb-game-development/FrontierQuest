using UnityEngine;

public class Enemy_Health : Entity_Health
{

    private Enemy enemy => GetComponent<Enemy>(); // defintion like this will call Get component evry time using the enemy verb


    public override bool TakeDamage(float damage,float elementalDamage,ElementalType element,
    Transform damageDealer)
    {
        bool sucssfullHit = base.TakeDamage(damage,elementalDamage,element, damageDealer);// need to fix from 3 
        if (!sucssfullHit) return false;
        if (isDead) return false;
        if (damageDealer.CompareTag("Player")) enemy.TryEnterBattleState(damageDealer);
        return true;
       
    }


}
