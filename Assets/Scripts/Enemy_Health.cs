using UnityEngine;

public class Enemy_Health : Entity_Health
{

    private Enemy enemy => GetComponent<Enemy>(); // defintion like this will call Get component evry time using the enemy verb


    public override void TakeDamage(float damage, Transform damageDealer)
    {
        base.TakeDamage(damage, damageDealer);
        if (isDead) return;
        if (damageDealer.CompareTag("Player")) enemy.TryEnterBattleState(damageDealer);
       
    }


}
