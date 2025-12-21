using UnityEngine;

public class Player_FallState : Player_AirState
{
    public Player_FallState(Player player, StateMachine stateMachine, string animBoolName)
        : base(player, stateMachine, animBoolName)
    {
    }


    public override void Update()
    {
        base.Update();
        Debug.Log("started falll state");
        if (player.groundDetected)
            stateMachine.changeState(player.idleState);

        if (player.wallDetected)
            
            stateMachine.changeState(player.wallSlideState);
        
    }
}
