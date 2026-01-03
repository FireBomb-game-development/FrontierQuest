using UnityEngine;

public class Player_WallJumpState : PlayerState
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

        // allow player go to jump attack state before falling
        if (input.Player.Attack.WasPressedThisFrame())
        {
            stateMachine.ChangeState(player.jumpAttackState);
        }
        if (rb.linearVelocity.y < 0 && stateMachine.CurrentState != player.jumpAttackState)
        {
            stateMachine.ChangeState(player.fallState);
        }
        if (player.groundDetected)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

}
