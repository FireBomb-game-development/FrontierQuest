using Unity.VisualScripting;
using UnityEngine;

public abstract class EntityState
{
    protected Player player;
    protected StateMachine stateMachine;
    protected string animBoolName;
    protected Animator anim;
    protected Rigidbody2D rb;
    protected PlayerInputSet input;
    protected bool triggerCalled;

    protected float stateTimer;
    public EntityState(Player player, StateMachine stateMachine, string animBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
        anim = player.GetComponentInChildren<Animator>();
        rb = player.rb;
        input = player.input;
    }
    // evrytime state will be changed, enter function will be called 
    public virtual void Enter()
    {
        anim.SetBool(animBoolName, true);
        triggerCalled = false;

    }
    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
        anim.SetFloat("yVelocity", rb.linearVelocity.y);
        if (input.Player.Dash.WasPressedThisFrame()&& CanDash())
        {
            stateMachine.changeState(player.dashState);
        }
    }
    // every time state is exited and change to new one, this function will be called
    public virtual void Exit()
    {
        anim.SetBool(animBoolName, false);
    }

    public void CallAnimationTrigger()
    {
        triggerCalled = true;
    }

    private bool CanDash()
    {
        if (player.wallDetected) return false;
        if (stateMachine.CurrentState == player.dashState) return false;


        return true;
    }
}
