using System;

public class PlayerAttackedState : PlayerState
{
   
    private Type m_LastType;
    
    public PlayerAttackedState(string animName) : base(animName)
    {
    }

    public override void OnEnter(FSM<Player> fsm)
    {
        base.OnEnter(fsm);
        m_LastType = fsm.Data as Type;
    }

    public override void OnUpdate(FSM<Player> fsm)
    {
        base.OnUpdate(fsm);

        if(!player.IsInvincible)
        {
            fsm.ChangeState(m_LastType);
        }
    }

    public static PlayerAttackedState Create(string animName)
    {
        return new PlayerAttackedState(animName);
    }
}