using System;
using GameFramework.Fsm;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(string animBoolName) : base(animBoolName)
    {
    }

    protected internal override void OnUpdate(IFsm<Player> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
        if (m_IsChanged) return;

        else if (m_Player.IsGroundDetected())
        {
            if (m_Player.IsTryAttack)
                ChangeState<PlayerAttackState>(fsm);
            else if (m_Player.IsTryJump)
                ChangeState<PlayerJumpState>(fsm);
        }
        else
        {
            m_Player.ResetCoyoteTimer();
            ChangeState<PlayerAirState>(fsm);
        }

    }
}