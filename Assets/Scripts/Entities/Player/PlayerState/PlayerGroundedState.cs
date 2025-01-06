using System;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player _player, string _animBoolName) : base(_player, _animBoolName)
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

        if (isFinished) return;

        else if (player.IsGroundDetected())
        {
            if (player.IsTryAttack)
                player.stateMachine.ChangeState(typeof(PlayerAttackState));
            else if (player.IsTryJump)
                player.stateMachine.ChangeState(typeof(PlayerJumpState));

        }
        else
        {
            player.ResetBufferCoyoteTimer();
            player.stateMachine.ChangeState(typeof(PlayerAirState));
        }
    }
}