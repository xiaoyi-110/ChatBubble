using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : IFSMState
{
    protected bool isFinished;
    protected Player player;

    protected Rigidbody2D rb;

    protected float inputHorizontal;
    protected float moveSpeedScale;
    protected bool triggerCalled;
    private readonly string animBoolName;

    public PlayerState(Player _player , string _animBoolName)
    {
        player = _player;
        animBoolName = _animBoolName;
        moveSpeedScale = 1f;
    }

    public virtual void Enter()
    {
        player.animator.SetBool(animBoolName, true);
        isFinished = false;
        triggerCalled = false;
        rb = player.rb;
        
    }

    public virtual void Exit()
    {
        player.animator.SetBool(animBoolName, false);
        isFinished = true;
    }

    public virtual void Update()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        player.SetVelocity(moveSpeedScale * inputHorizontal * player.moveSpeed, rb.velocity.y);

        player.animator.SetFloat("yVelocity", rb.velocity.y);
        
    }

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }

    

}
