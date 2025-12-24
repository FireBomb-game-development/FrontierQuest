using Unity.Mathematics;
using UnityEngine;

public class Enemy_BattleState : EnemyState
{
    private Transform player;
    public Enemy_BattleState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }


    public override void Update()
    {
        base.Update();
        if (WithinAttackRange()) stateMachine.changeState(enemy.attackState);
        else enemy.SetVelocity(enemy.battleMoveSpeed * DirectionToPlayer(), rb.linearVelocity.y);
    }

    public override void Enter()
    {
        base.Enter();
        if (player == null) player = enemy.PlayerDetection().transform;

    }
    private bool WithinAttackRange() => DistanceToPlayer() < enemy.attackDistance;

    private float DistanceToPlayer()
    {
        if (player == null) return float.MaxValue;
        else return math.abs(player.position.x - enemy.transform.position.x);
    }
    private int DirectionToPlayer()
    {
        if (player == null) return 0;
        return player.position.x > enemy.transform.position.x ? 1 : -1;
    }
}
