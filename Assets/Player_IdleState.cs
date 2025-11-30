using UnityEngine;

public class Player_IdleState : Player_GroundedState
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Player_IdleState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {
        // base.Update();

    }
    public override void Enter()
    {
        base.Enter();
        player.SetVelocity(0, rb.linearVelocity.y);
    }
    public override void Update()
    {
        base.Update();
        if (player.moveInput.x != 0)
            stateMachine.changeState(player.moveState);

        


    }

}
