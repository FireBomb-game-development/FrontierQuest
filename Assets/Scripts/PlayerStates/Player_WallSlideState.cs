using System.Numerics;
using System.Security;
using UnityEngine;

public class Player_WallSlideState : EntityState
{
    public Player_WallSlideState(Player player, StateMachine stateMachine, string animBoolName)
        : base(player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();
        HandleWallSlide();

        if (input.Player.Jump.WasPressedThisFrame())
        {
            stateMachine.changeState(player.wallJumpState);
        }

        if (player.wallDetected == false)
        {
            stateMachine.changeState(player.fallState);
        }
        if (player.groundDetected)
        {
            stateMachine.changeState(player.idleState);
            player.Flip();
        }

    }
    public void HandleWallSlide()
    {
        if (player.moveInput.y < 0)
        {
            player.SetVelocity(player.moveInput.x, rb.linearVelocity.y);

        }
        else
        {
            player.SetVelocity(player.moveInput.x, rb.linearVelocity.y * player.wallSlideWallMultipler);
        }
    }

}
