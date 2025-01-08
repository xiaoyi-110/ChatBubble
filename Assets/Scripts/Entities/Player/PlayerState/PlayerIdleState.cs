using System;
using System.Collections;
using System.Collections.Generic;
using GameFramework.Fsm;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(string animBoolName) : base(animBoolName)
    {
    }

    protected internal override void OnUpdate(IFsm<Player> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);

        if(m_IsChanged)return;
        if(Math.Abs(m_InputHorizontal) > 0.1f)
        {
            ChangeState<PlayerMoveState>(fsm);
        }
    }

}
