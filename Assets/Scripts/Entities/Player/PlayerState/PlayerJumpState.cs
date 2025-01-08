using GameFramework.Fsm;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(string animBoolName) : base(animBoolName)
    {
    }

    protected internal override void OnEnter(IFsm<Player> fsm)
    {
        base.OnEnter(fsm);
        m_Player.SetVelocity(m_Rb.velocity.x, m_Player.JumpForce);
        m_Player.ClearCoyoteTimer();
    }

    protected internal override void OnUpdate(IFsm<Player> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
        if(m_IsChanged) return;

        if(m_Rb.velocity.y < 0)
        {
            ChangeState<PlayerAirState>(fsm);
        }
    }
    
}