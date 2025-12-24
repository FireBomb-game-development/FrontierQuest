using UnityEngine;

public class EnemyState : EntityState
{


    protected Enemy enemy;

    public EnemyState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(stateMachine, animBoolName)
    {
        this.enemy = enemy;
        rb = enemy.rb;
        anim = enemy.GetComponentInChildren<Animator>();
    }

    public override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.F)) stateMachine.changeState(enemy.attackState);
        anim.SetFloat("moveAnimSpeedMultiplier", enemy.moveAnimSpeedMultiplier);
        anim.SetFloat("xVelocity", rb.linearVelocityX);
    }


}
