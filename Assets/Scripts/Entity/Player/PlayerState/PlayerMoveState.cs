
using System;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(string animBoolName) : base(animBoolName)
    {
    }

    public override void OnUpdate(FSM<Player> fsm)
    {
        base.OnUpdate(fsm);
        if(m_IsChanged) return;

        if(Math.Abs(m_InputHorizontal) < 0.01f)
        {
            ChangeState<PlayerIdleState>(fsm);
        }
    }

    public static PlayerMoveState Create()
    {
        return new PlayerMoveState("Move");
    }
}