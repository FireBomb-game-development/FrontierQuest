using UnityEngine;

public class Player_JumpAttackState : EntityState
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private bool touchGround;
    public Player_JumpAttackState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {

    }
    public override void Enter()
    {
        base.Enter();
        touchGround = false;
        player.SetVelocity(player.jumpAttackVelocity.x * player.facingDiraction,player.jumpAttackVelocity.y);
    }
    public override void Update()
    {
        base.Update();
        if (player.groundDetected && touchGround ==false)
        {
            touchGround = true;
            anim.SetTrigger("jumpAttackTrigger");
            player.SetVelocity(0, rb.linearVelocity.y);
        }
        if (triggerCalled && player.groundDetected) stateMachine.changeState(player.idleState);
    }

    
}
