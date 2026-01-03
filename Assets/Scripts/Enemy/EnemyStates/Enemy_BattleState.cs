using Unity.Mathematics;
using UnityEngine;

public class Enemy_BattleState : EnemyState
{
    private Transform player;
    [SerializeField] private float lastTimeWasInBattle;
    public Enemy_BattleState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }




    public override void Enter()
    {
        base.Enter();
        UpdateBattleTimer();
        player ??= enemy.GetPlayerRefernce().transform; // assign if player is null

        if (ShouldRetreat())
        {
            rb.linearVelocity = new Vector2(enemy.retreatVelocity.x * -DirectionToPlayer(), enemy.retreatVelocity.y);
            enemy.HandleFlip(DirectionToPlayer());
        }


    }

    public override void Update()
    {
        base.Update();

        if (BattleTimeIsOver()) stateMachine.ChangeState(enemy.idleState);
        if (PlayerWithinAttackRange() && enemy.PlayerDetection()) stateMachine.ChangeState(enemy.attackState);
        else GetCloserToPlayer();
    }
    private bool BattleTimeIsOver() => Time.time > lastTimeWasInBattle + enemy.battleTimeDuration;
    private void UpdateBattleTimer() => lastTimeWasInBattle = Time.time;
    private bool PlayerWithinAttackRange() => DistanceToPlayer() < enemy.attackDistance;
    private void GetCloserToPlayer() { enemy.SetVelocity(enemy.battleMoveSpeed * DirectionToPlayer(), rb.linearVelocity.y); }
    private bool ShouldRetreat() => DistanceToPlayer() < enemy.minRetreatDistance;

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
