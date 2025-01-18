
using System;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(string animName) : base(animName)
    {
    }

    public override void OnUpdate(FSM<Player> fsm)
    {
        base.OnUpdate(fsm);
        if(m_HasChanged)return;

        if(!m_Player.isTryAir)
        {
            fsm.ChangeState<PlayerFallState>();
        }
    }

    override public void OnEnter(FSM<Player> fsm)
    {
        base.OnEnter(fsm);
        fsm.Data = typeof(PlayerAirState);
    }
    public static PlayerAirState Create(string animName)
    {
        return new PlayerAirState(animName);
    }
}

