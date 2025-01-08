using GameFramework.Fsm;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(string animBoolName) : base(animBoolName)
    {
    }

    protected internal override void OnUpdate(IFsm<Player> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);

        if(m_Player.IsTryJumpAtCoyoteTime)
        {
            ChangeState<PlayerJumpState>(fsm);
        } 
        else if(m_Player.IsGroundDetected())
        {
            ChangeState<PlayerIdleState>(fsm);
        }
    }
}