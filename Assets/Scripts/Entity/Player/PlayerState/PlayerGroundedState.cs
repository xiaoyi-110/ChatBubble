using System;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(string animBoolName) : base(animBoolName)
    {
    }

    public override void OnUpdate(FSM<Player> fsm)
    {
        base.OnUpdate(fsm);
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