
using System;
using GameFramework.Fsm;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(string animBoolName) : base(animBoolName)
    {
    }

    protected internal override void OnUpdate(IFsm<Player> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
        if(m_IsChanged) return;

        if(Math.Abs(m_InputHorizontal) < 0.01f)
        {
            ChangeState<PlayerIdleState>(fsm);
        }
    }
}