using Unity.VisualScripting;
using UnityEngine;

public abstract class PlayerState :EntityState
{
    protected Player player;
    protected PlayerInputSet input;

    public PlayerState(Player player, StateMachine stateMachine, string animBoolName) : base(stateMachine, animBoolName)
    {
        this.player = player;
        anim = player.GetComponentInChildren<Animator>();
        rb = player.rb;
        input = player.input;
    }


    public override void Update()
    {
        base.Update();
        anim.SetFloat("yVelocity", rb.linearVelocity.y);
        if (input.Player.Dash.WasPressedThisFrame()&& CanDash())stateMachine.changeState(player.dashState);

    }

    private bool CanDash()
    {
        if (player.wallDetected) return false;
        if (stateMachine.CurrentState == player.dashState) return false;


        return true;
    }
}
