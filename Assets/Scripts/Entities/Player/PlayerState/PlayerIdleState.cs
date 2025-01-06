using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player _player, string _animBoolName) : base(_player, _animBoolName)
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

        if(isFinished)return;
        if(Math.Abs(inputHorizontal) > 0.1f)
        {
            player.stateMachine.ChangeState(typeof(PlayerMoveState));
        }
        
        
    }
}
