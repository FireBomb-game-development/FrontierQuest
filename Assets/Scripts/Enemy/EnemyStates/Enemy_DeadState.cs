using UnityEngine;

public class Enemy_DeadState : EnemyState
{
    private Collider2D col;
    private Transform player;
    private Vector2 knockdead = new Vector2(5, 15);
    public Enemy_DeadState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        col = enemy.GetComponent<Collider2D>();
       
        
    }


    public override void Enter()
    {
         player = enemy.GetPlayerRefernce();
        anim.enabled = false;
        col.enabled = false;
        rb.gravityScale = 12;
        int diretion = enemy.transform.position.x > player.position.x ? 1 : -1;
        if (player != null) rb.linearVelocity = new Vector2(knockdead.x * diretion, knockdead.y);
        else rb.linearVelocity = new Vector2(rb.linearVelocity.x, knockdead.y);
        stateMachine.SwitchOffStateMachine();
        
    }
}
