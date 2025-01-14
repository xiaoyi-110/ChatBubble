using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(string animBoolName) : base(animBoolName)
    {
    }

    public override void OnUpdate(FSM<Player> fsm)
    {
        base.OnUpdate(fsm);

        if(m_IsChanged)return;
        if(Math.Abs(m_InputHorizontal) > 0.1f)
        {
            ChangeState<PlayerMoveState>(fsm);
        }
    }

    public static PlayerIdleState Create()
    {
        PlayerIdleState state = new PlayerIdleState("Idle");
        return state;
    }

}
