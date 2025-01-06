
using System;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player _player, string _animBoolName) : base(_player, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        
    }

    public override void Update()
    {
        base.Update();

        if(isFinished) return;
        if(Math.Abs(inputHorizontal) < 0.01f)
        {
            player.stateMachine.ChangeState(typeof(PlayerIdleState));
        }
    }

    
}