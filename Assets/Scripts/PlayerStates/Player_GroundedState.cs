using UnityEngine;

public class Player_GroundedState : EntityState
{



    public Player_GroundedState(Player player, StateMachine stateMachine, string animBoolName)
        : base(player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();
        if (rb.linearVelocity.y < 0 && !player.groundDetected)
        {
            stateMachine.changeState(player.fallState);
        }
        if (input.Player.Jump.WasPerformedThisFrame())
        {
            stateMachine.changeState(player.jumpState);
        }
        if( input.Player.Attack.WasPerformedThisFrame()){
            stateMachine.changeState(player.basicAttackState);
            
        }
    }


}
