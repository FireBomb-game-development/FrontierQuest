using UnityEngine;

public class Player_WallJumpState : EntityState
{

    public Player_WallJumpState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }


    public override void Enter()
    {
        base.Enter();
        player.SetVelocity(player.wallJumpForce.x * -player.facingDiraction, player.wallJumpForce.y);
    }

    public override void Update()
    {
        base.Update();
        if (rb.linearVelocity.y < 0)
        {
            stateMachine.changeState(player.fallState);
        }
        if (player.groundDetected)
        {
            stateMachine.changeState(player.idleState);
        }
    }

}
